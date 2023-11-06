using System;
using System.Collections.Generic;

namespace EsMasBarato.Entidades.Modelos
{
    public partial class Producto
    {
        public Producto()
        {
            Reseñas = new HashSet<Reseña>();
        }

        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal PrecioRegular { get; set; }
        public decimal PrecioWeb { get; set; }
        public int IdComercio { get; set; }
        public int IdCategoria { get; set; }
        public string CodigoBarra { get; set; } = null!;
        public bool? Activo { get; set; }
        public bool? Anunciado { get; set; }
        public bool? Valoracion { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        public virtual Comercio IdComercioNavigation { get; set; } = null!;
        public virtual ICollection<Reseña> Reseñas { get; set; }
    }
}
