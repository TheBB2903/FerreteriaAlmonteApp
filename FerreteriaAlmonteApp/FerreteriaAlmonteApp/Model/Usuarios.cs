using System;
using System.Collections.Generic;
using System.Text;

namespace FerreteriaAlmonteApp.Model
{
    public class Usuarios
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string TipoUsuario { get; set; }
    }
}
