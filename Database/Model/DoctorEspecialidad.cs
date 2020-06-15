using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class DoctorEspecialidad
    {
        public int IdDoctor { get; set; }
        public int IdEspecialidad { get; set; }

        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Especialidad IdEspecialidadNavigation { get; set; }
    }
}
