using EsMasBarato.EF.Context;

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
