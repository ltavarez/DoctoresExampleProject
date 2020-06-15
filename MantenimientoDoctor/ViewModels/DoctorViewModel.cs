using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MantenimientoDoctor.ViewModels
{
    public class DoctorViewModel
    {

        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Correo { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { get; set; }

        [Display(Name = "Foto")]
        public IFormFile Photo { get; set; }

        [Display(Name = "Especialidades")]
        public List<int> EspecialidadIds { get; set; }

        public List<EspecialidadViewModel> Especialidades { get; set; }

        public string ProfilePhoto { get; set; }


    }
}
