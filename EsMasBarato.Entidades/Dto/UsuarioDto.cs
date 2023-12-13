
namespace EsMasBarato.Entidades.Dto
{
    public class UsuarioDto
    {

        public string Nombre { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;                 
        public string Email { get; set; } = string.Empty;
        public int IdRol { get; set; } = 0;
                   
    }
}
