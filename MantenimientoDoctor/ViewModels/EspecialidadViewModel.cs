using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MantenimientoDoctor.ViewModels
{
    public class EspecialidadViewModel
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

    }
}
