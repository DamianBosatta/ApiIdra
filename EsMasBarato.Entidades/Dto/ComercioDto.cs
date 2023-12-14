
namespace EsMasBarato.Entidades.Dto
{
    public class ComercioDto
    {
        public int IdComercio { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int? IdCategoria { get; set; }
        public string? NombreContacto { get; set; }
        public int? NumeroTelefono { get; set; }
        public int? IdRol { get; set; }
        public int? Valoracion { get; set; }
        
    }
}
