using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;

namespace EsMasBarato.Negocios.Negocios.NegociosReseña
{
    public interface INegocioReseña:INegocioGenerico<Reseña>
    {
        public Task<List<ReseñaRespuesta>> GetReseñas();
    }
}
