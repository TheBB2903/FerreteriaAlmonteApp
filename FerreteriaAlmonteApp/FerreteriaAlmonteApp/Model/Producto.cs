using System;
using System.Collections.Generic;
using System.Text;

namespace FerreteriaAlmonteApp.Model
{
    public class Producto
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public float Existencia { get; set; }
        public float Precio { get; set; }
        public string Id_Categoria { get; set; }
        public string Image { get; set; }

    }
}
