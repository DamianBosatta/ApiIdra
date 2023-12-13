using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.NegociosGenericos;
using Serilog;

namespace EsMasBarato.Negocios.Negocios.NegociosValoracion
{
    public class NegocioValoracion:NegocioGenerico<Valoracion>,INegocioValoracion
    {
        public NegocioValoracion(ILogger logger) : base(logger)
        {

        }
    }
}
