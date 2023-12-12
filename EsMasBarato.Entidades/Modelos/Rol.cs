using System;
using System.Collections.Generic;

namespace EsMasBarato.Api.Modelos
{
    public partial class Rol
    {
        public Rol()
        {
            Comercios = new HashSet<Comercio>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string? TipoRol { get; set; }

        public virtual ICollection<Comercio> Comercios { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
