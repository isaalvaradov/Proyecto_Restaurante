using System.ComponentModel.DataAnnotations;

namespace Proyecto_Restaurante.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe responder el captcha")]
        public string Captcha { get; set; }
    }
}