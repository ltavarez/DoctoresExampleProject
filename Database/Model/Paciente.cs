using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class Paciente
    {
        public Paciente()
        {
            PacienteTelefono = new HashSet<PacienteTelefono>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public string NumeroCasa { get; set; }
        public string Sector { get; set; }
        public string Ciudad { get; set; }

        public virtual ICollection<PacienteTelefono> PacienteTelefono { get; set; }
    }
}
