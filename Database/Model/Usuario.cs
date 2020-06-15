using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioDoctor = new HashSet<UsuarioDoctor>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UsuarioDoctor> UsuarioDoctor { get; set; }
    }
}
