using System;
using System.Collections.Generic;

namespace EsMasBarato.Api.Modelos
{
    public partial class CategoriaComercio
    {
        public CategoriaComercio()
        {
            Comercios = new HashSet<Comercio>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Comercio> Comercios { get; set; }
    }
}
