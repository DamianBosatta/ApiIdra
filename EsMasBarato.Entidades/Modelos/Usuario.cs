using System;
using System.Collections.Generic;

namespace EsMasBarato.Entidades.Modelos
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ulong? Borrado { get; set; }
        public int? IdRol { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
    }
}
