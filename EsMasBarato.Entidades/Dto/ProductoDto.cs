using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.Dto
{
    public class ProductoDto
    {
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
    }
}
