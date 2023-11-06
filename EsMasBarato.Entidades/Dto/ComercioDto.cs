using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.Dto
{
    public class ComercioDto
    {
        public int? IdComercio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int? Categoria { get; set; }
        public string? NombreContacto { get; set; }
        public int? NumeroTelefono { get; set; }
    }
}
