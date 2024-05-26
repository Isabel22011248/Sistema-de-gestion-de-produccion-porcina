

using CPP1.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;

namespace CPP1.Controllers
{

    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Contact()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View("Send", model);
            }
            return View("Contact");
        }
        [HttpPost]
        public IActionResult Send(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Configurar los detalles del correo electrónico
                    var correo = new MailMessage();
                    correo.From = new MailAddress("depsaquijano@outlook.com");
                    correo.To.Add("depsaquijano@outlook.com");
                    correo.Subject = "Formulario de contacto";
                    correo.Body = $"{model.Nombre}\n{model.CorreoElectronico}\n{model.Mensaje}";

                    // Enviar el correo electrónico utilizando el servidor SMTP de Outlook.com
                    using (var smtp = new SmtpClient("smtp-mail.outlook.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("depsaquijano@outlook.com", "tEAMODEPSA");
                        smtp.EnableSsl = true;

                        smtp.Send(correo);
                    }
                    //Enviar un mensaje de Correo enviado exitosamente
                    ViewData["MessageSend"] = "Chido ya la hiciste";
                    return RedirectToAction("Contact", "Contact");
                }
                catch (Exception ex)
                {
                    // Pasar el mensaje de error a la vista Error.cshtml
                    ViewData["ErrorDetails"] = ex.Message;
                    return View("ErrorMail");
                }
            }
            return View("Contact");
        }
    }

    
}

    
