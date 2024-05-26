using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CPP1.Models;
using CPP1.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.crypto;



namespace CPP1.Controllers
{
    public class GestionBitacoraController : Controller
    {
        private readonly OriginalContext _DBContext;

        public GestionBitacoraController(OriginalContext dBContext)
        {
            _DBContext = dBContext;
        }


        /** Acción para mostrar la lista de bitácoras.
        Esta acción trae la lista de todas las bitácoras de corrales desde la base de datos y las muestra en la vista correspondiente.
        @returns Vista que muestra la lista de bitácoras.
        **/
        public IActionResult Bitacora()
        {
            List<BitacoraCorral> lista = _DBContext.BitacoraCorrals.Include(p => p.oCerdo)
                                                                  .Include(c => c.oCorral)
                                                                  .Include(a => a.oAlimento)
                                                                  .ToList();
            return View(lista);
        }
        /** Acción para mostrar el detalle de una bitácora específica.
        Esta acción trae la información de una bitácora de corral específica, así como las listas desplegables de cerdos, corrales y alimentos para que el usuario pueda editarla.
        @param idbitacora: ID de la bitácora a mostrar.
        @returns Vista que muestra el detalle de la bitácora.
        **/

        [HttpGet]
        public IActionResult Bitacora_Detalle(int idbitacora)
        {
            CorralVM oCorralVM = new CorralVM()
            {
                oBitacora = new BitacoraCorral(),
                oListaCerdo = _DBContext.Cerdos.Select(Cerdo => new SelectListItem()
                {
                    Text = Cerdo.Idcerdo.ToString(),
                    Value = Cerdo.Idcerdo.ToString()

                }).ToList(),

                oListaCorral = _DBContext.Corrales.Select(Corrale => new SelectListItem()
                {
                    Text = Corrale.Idcorral.ToString(),
                    Value = Corrale.Idcorral.ToString()
                }).Distinct().ToList(),

                oListaAlimento = _DBContext.TipoAlimentacions.Select(TipoAlimentacion => new SelectListItem()
                {
                    Text = TipoAlimentacion.Nombre.ToString(),
                    Value = TipoAlimentacion.Nombre.ToString()
                }).ToList(),
        };

            if (idbitacora != 0)
            {

                oCorralVM.oBitacora = _DBContext.BitacoraCorrals.Find(idbitacora);
            }


            return View(oCorralVM);
        }

        /** Acción para guardar los cambios en una bitácora.
        Esta acción se encarga de guardar los cambios realizados en una bitácora de corral, ya sea creando una nueva bitácora o actualizando una existente.
        @param oCorralVM: Modelo de vista que contiene la información de la bitácora a guardar.
        @returns Redirección a la acción de listado de bitácoras.
        **/


        [HttpPost]
        public IActionResult Bitacora_Detalle(CorralVM oCorralVM)
        {
            if (oCorralVM.oBitacora.Idbitacora == 0)
            {
                _DBContext.BitacoraCorrals.Add(oCorralVM.oBitacora);

        
            }
            else
            {
                _DBContext.BitacoraCorrals.Update(oCorralVM.oBitacora);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Bitacora", "GestionBitacora");
        }

        /** Acción para generar un reporte PDF de las bitácoras.
       Esta acción genera un reporte en formato PDF que contiene la lista de todas las bitácoras de corrales.
       @returns Archivo PDF con la lista de bitácoras.
       **/

        public ActionResult GenerarPDF()
        {
            List<BitacoraCorral> BitacoraCorrals = _DBContext.BitacoraCorrals
                                                                .Include(p => p.oCerdo)
                                                                .Include(c => c.oCorral)
                                                                .Include(a => a.oAlimento)
                                                                .ToList();

            var document = new Document(PageSize.A4);
            document.SetMargins(50, 50, 25, 25);
            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                var titulo = new Paragraph("Lista de bitácoras ", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                titulo.Alignment = Element.ALIGN_CENTER;
                document.Add(titulo);

                // Crear la tabla con anchos de columna personalizados
                var tabla = new PdfPTable(6);
                tabla.DefaultCell.Padding = 5;
                tabla.WidthPercentage = 100;
                tabla.HorizontalAlignment = Element.ALIGN_LEFT;

                // Fuente y tamaño de la fuente
                var font = new iTextSharp.text.Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);

                tabla.AddCell(new Phrase("No.Renglon", font));
                tabla.AddCell(new Phrase("No.Corral", font));
                tabla.AddCell(new Phrase("Tamaño", font));
                tabla.AddCell(new Phrase("Alimento", font));
                tabla.AddCell(new Phrase("Cantidad", font));
                tabla.AddCell(new Phrase("Fecha de registro", font));

                foreach (var bitacoraCorral in BitacoraCorrals)
                {
                    tabla.AddCell(new Phrase(bitacoraCorral.Idbitacora.ToString(), font));
                    tabla.AddCell(new Phrase(bitacoraCorral.Idcorral.ToString(), font));
                    tabla.AddCell(new Phrase(bitacoraCorral.oCerdo.Tamaño, font));
                    tabla.AddCell(new Phrase(bitacoraCorral.oAlimento.Nombre, font));
                    tabla.AddCell(new Phrase(bitacoraCorral.Cantidad.ToString(), font));
                    tabla.AddCell(new Phrase(bitacoraCorral.FechaRegistro.ToString("yyyy/MM/dd"), font));
                }

                document.Add(tabla);

                document.Close();

                var fileContents = memoryStream.ToArray();
                return File(fileContents, "application/pdf", "bitacoracorral.pdf");
            }
        }

    }

}