using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class Doctor
    {
        public Doctor()
        {
            DoctorEspecialidad = new HashSet<DoctorEspecialidad>();
            UsuarioDoctor = new HashSet<UsuarioDoctor>();
        }

        public int Id { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CodigoPostal { get; set; }
        public string ProfilePhoto { get; set; }

        public virtual ICollection<DoctorEspecialidad> DoctorEspecialidad { get; set; }
        public virtual ICollection<UsuarioDoctor> UsuarioDoctor { get; set; }
    }
}
