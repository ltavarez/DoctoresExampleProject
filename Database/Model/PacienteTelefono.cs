using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class PacienteTelefono
    {
        public int IdPaciente { get; set; }
        public string Telefonos { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; }
    }
}
