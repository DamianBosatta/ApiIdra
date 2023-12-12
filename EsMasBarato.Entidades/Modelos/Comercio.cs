using System;
using System.Collections.Generic;

namespace EsMasBarato.Api.Modelos
{
    public partial class Comercio
    {
        public Comercio()
        {
            Productos = new HashSet<Producto>();
            ValoracionComercios = new HashSet<ValoracionComercio>();
        }

        public int IdComercio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int? IdCategoria { get; set; }
        public string? NombreContacto { get; set; }
        public int? NumeroTelefono { get; set; }
        public int? IdRol { get; set; }
        public int? Valoracion { get; set; }

        public virtual CategoriaComercio? IdCategoriaNavigation { get; set; }
        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<ValoracionComercio> ValoracionComercios { get; set; }
    }
}
