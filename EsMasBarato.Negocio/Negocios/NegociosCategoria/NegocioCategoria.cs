
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.NegociosGenericos;
using Serilog;

namespace EsMasBarato.Negocios.Negocios.NegociosCategoria
{
    public class NegocioCategoria:NegocioGenerico<Categoria>,INegocioCategoria
    {

        public NegocioCategoria(ILogger logger) : base(logger)
        {


        }


    }
}
