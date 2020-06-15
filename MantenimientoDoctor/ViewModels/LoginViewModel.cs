using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoDoctor.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
