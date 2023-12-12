using System;
using System.Collections.Generic;

namespace EsMasBarato.Api.Modelos
{
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = null!;
        public ulong? Borrado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
