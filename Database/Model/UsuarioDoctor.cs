using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class UsuarioDoctor
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
     
    }
}
