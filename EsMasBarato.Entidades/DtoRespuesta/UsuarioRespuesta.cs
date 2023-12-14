

namespace EsMasBarato.Entidades.DtoRespuesta
{
    public class UsuarioRespuesta
    {
        public int Id { get; set; }
 
        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? IdRol { get; set; }

        public string TipoRol {  get; set; } = string.Empty;

        public int IdComercio { get; set; } = 0;

        public string TipoComercio { get; set; }= string.Empty;



    }
}
