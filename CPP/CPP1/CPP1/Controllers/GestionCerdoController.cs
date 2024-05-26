using Microsoft.AspNetCore.Mvc;
using CPP1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using CPP1.Models.ViewModels;
using System.Drawing.Printing;
using System.Drawing;
using System.Xml.Linq;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.WebUtilities;
using System.Globalization;



namespace CPP1.Controllers
{
    public class GestionCerdoController : Controller
    {

        private readonly OriginalContext _DBContext;

        public GestionCerdoController(OriginalContext dBContext)
        {
            _DBContext = dBContext;
        }
        public IActionResult Cerdo()
        {
            List<Cerdo> lista = _DBContext.Cerdos.ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Cerdo_Detalle(int idcerdo)
        {
            CerdoVM oCerdoVM = new CerdoVM()
            {
                oCerdo = new Cerdo(),

            };

            if (idcerdo != 0)
            {

                oCerdoVM.oCerdo = _DBContext.Cerdos.Find(idcerdo);
            }


            return View(oCerdoVM);
        }

        [HttpPost]
        public IActionResult Cerdo_Detalle(CerdoVM oCerdoVM)
        {
            if (oCerdoVM.oCerdo.Idcerdo == 0)
            {
                _DBContext.Cerdos.Add(oCerdoVM.oCerdo);

            }
            else
            {
                _DBContext.Cerdos.Update(oCerdoVM.oCerdo);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Cerdo", "GestionCerdo");
        }
    }
}
