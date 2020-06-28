using System;
using System.Collections.Generic;

namespace Database.Model
{
    public partial class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}
