using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;

namespace EsMasBarato.Negocios.Negocios.NegociosValoracion
{
   public interface INegocioValoracionComercio:INegocioGenerico<ValoracionComercio>
    {
        public Task<List<ValoracionComercioRespuesta>> GetValoracionComercios();
    }
}
