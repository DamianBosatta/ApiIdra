using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Entidades.DtoRespuesta
{
    public class LoginRespuesta
    {
        public int? Id { get; set; }

        public string? Nombre { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Token { get; set; }

        public int? IdRol { get; set; }

        public int? idComercio { get; set; }

    }
}
