using System;
using System.Collections.Generic;

namespace EsMasBarato.Entidades.Modelos
{
    public partial class Reseña
    {
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int IdValoracion { get; set; }
        public string? Comentario { get; set; }
        public int Id { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Valoracion IdValoracionNavigation { get; set; } = null!;
    }
}
