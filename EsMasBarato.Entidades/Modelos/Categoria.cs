using System;
using System.Collections.Generic;

namespace EsMasBarato.Entidades.Modelos
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public int Descripcion { get; set; }
        public ulong? Borrado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
