using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.NegociosGenericos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Negocios.Negocios.NegociosRol
{
    public class NegocioRol : NegocioGenerico<Rol>, INegociosRol
    {
        public NegocioRol(ILogger logger) : base(logger)
        { 
        
        }

    }
}