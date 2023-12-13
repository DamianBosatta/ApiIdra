using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;


namespace EsMasBarato.Negocios.Negocios.NegociosUsuario
{
    public interface INegocioUsuario:INegocioGenerico<Usuario>
    {
        public Task<List<UsuarioRespuesta>> GetUsuarios();
    }
}
