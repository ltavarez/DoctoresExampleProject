using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoDoctor.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Correo { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Compare("Password",ErrorMessage = "No coinciden las contraseñas")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
