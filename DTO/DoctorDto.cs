using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CodigoPostal { get; set; }
        public string ProfilePhoto { get; set; }
        public List<EspecialidadDto> Especialidades { get; set; }


    }
}
