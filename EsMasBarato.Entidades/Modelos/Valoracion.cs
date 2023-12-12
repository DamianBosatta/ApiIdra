using System;
using System.Collections.Generic;

namespace EsMasBarato.Api.Modelos
{
    public partial class Valoracion
    {
        public Valoracion()
        {
            Reseñas = new HashSet<Reseña>();
            ValoracionComercios = new HashSet<ValoracionComercio>();
        }

        public int IdValoracion { get; set; }
        public string Descripcion { get; set; } = null!;
        public ulong Borrado { get; set; }

        public virtual ICollection<Reseña> Reseñas { get; set; }
        public virtual ICollection<ValoracionComercio> ValoracionComercios { get; set; }
    }
}
