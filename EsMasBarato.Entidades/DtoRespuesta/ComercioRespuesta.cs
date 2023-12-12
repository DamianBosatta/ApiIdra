using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.DtoRespuesta
{
    public class ComercioRespuesta
    {

        public int IdComercio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int? IdCategoria { get; set; }
        public string DescripcionCategoria { get; set; } = string.Empty;
        public string? NombreContacto { get; set; }
        public int? NumeroTelefono { get; set; }
        public int? IdRol { get; set; }
        public string DescripcionRol { get; set; } = string.Empty;
        public int? Valoracion { get; set; }
    }
}
