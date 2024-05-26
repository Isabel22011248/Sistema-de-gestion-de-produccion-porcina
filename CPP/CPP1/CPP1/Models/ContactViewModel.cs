using System.ComponentModel.DataAnnotations;

namespace CPP1.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Correo electrónico no es una dirección de correo electrónico válida.")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo Mensaje es obligatorio.")]
        public string? Mensaje { get; set; }
    }
}
