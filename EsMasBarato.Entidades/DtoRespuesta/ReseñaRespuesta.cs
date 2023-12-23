using EsMasBarato.Api.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.DtoRespuesta
{
    public class ReseñaRespuesta
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int IdProducto { get; set; }
        public string DescripcionProducto { get; set; } = string.Empty;
        public int IdValoracion { get; set; }
        public string? Comentario { get; set; }
        public int Id { get; set; } = 0; 
        public Producto? producto { get; set; }
        public Comercio? comercio { get; set; }
    }
}
