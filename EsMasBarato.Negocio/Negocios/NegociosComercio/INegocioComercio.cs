using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;


namespace EsMasBarato.Negocios.Negocios.NegociosComercio
{
    public interface INegocioComercio:INegocioGenerico<Comercio>
    {
        public Task<List<ComercioRespuesta>> GetComercios(int idComercio);

        public Task<object?> ObtenerPromedioValoracionComercio(int idComercio);
    }
}
