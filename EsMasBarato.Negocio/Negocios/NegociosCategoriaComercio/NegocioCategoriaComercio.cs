using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.NegociosGenericos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Negocios.Negocios.NegociosCategoriaComercio
{
    public class NegocioCategoriaComercio: NegocioGenerico<CategoriaComercio>,INegocioCategoriaComercio
    {
        public NegocioCategoriaComercio(ILogger logger) : base(logger)
        {


        }
    }
}
