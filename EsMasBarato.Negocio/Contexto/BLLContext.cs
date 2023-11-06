using EsMasBarato.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Negocios.Contexto
{
    public abstract class BLLContext : IDisposable
    {
        protected ContextEsMasBarato Context;

        public BLLContext()
        {
            EFStringConnection.GetStringConnection();
            this.Context = new ContextEsMasBarato(EFStringConnection.StringConnection!);
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
