using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using Serilog;

namespace EsMasBarato.Negocios.Negocios.NegociosValoracion
{
    public class NegocioValoracionComercio:NegocioGenerico<ValoracionComercio>,INegocioValoracionComercio
    {
        public NegocioValoracionComercio(ILogger logger) : base(logger)
        {
        }

        public Task<List<ValoracionComercioRespuesta>> GetValoracionComercios()
        {
            try { 
            var query = (from valComercio in Context.ValoracionComercios
                         join valoracion in Context.Valoracions
                         on valComercio.IdValoracion equals valoracion.IdValoracion
                         join comercio in Context.Comercios
                         on valComercio.IdComercio equals comercio.IdComercio
                         select new ValoracionComercioRespuesta
                         {
                             Id= valComercio.Id,
                             IdValoracion= valComercio.IdValoracion,
                             DescripcionValoracion= valoracion.Descripcion,
                             Comentario= valComercio.Comentario,
                             IdComercio=valComercio.IdComercio,
                             DescripcionComercio= comercio.Nombre

                         });



            return (Task<List<ValoracionComercioRespuesta>>)query;
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioValoracionComercio" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetValoracionComercios(NegocioValoracionComercio)");
            }
        }
    }
}
