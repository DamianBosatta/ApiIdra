using System;
using System.Collections.Generic;

namespace EsMasBarato.Entidades.Modelos
{
    public partial class ValoracionComercio
    {
        public int Id { get; set; }
        public int? IdValoracion { get; set; }
        public string? Comentario { get; set; }
        public int? IdComercio { get; set; }

        public virtual Comercio? IdComercioNavigation { get; set; }
        public virtual Valoracion? IdValoracionNavigation { get; set; }
    }
}
