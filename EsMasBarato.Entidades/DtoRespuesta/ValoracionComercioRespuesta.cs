using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.DtoRespuesta
{
    public class ValoracionComercioRespuesta
    {
        public int Id { get; set; } = 0;
        public int? IdValoracion { get; set; }
        public string DescripcionValoracion { get; set; }= string.Empty;
        public string? Comentario { get; set; }
        public int? IdComercio { get; set; }
        public string DescripcionComercio { get; set; } = string.Empty;


    }
}
