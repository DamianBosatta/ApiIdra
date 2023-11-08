using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.Dto
{
    public class ValoracionComercioDto
    {
        public int Id { get; set; }
        public int? IdValoracion { get; set; }
        public string? Comentario { get; set; }
        public int? IdComercio { get; set; }
    }
}
