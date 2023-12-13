using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Negocios.Negocios.NegociosReseña
{
    public class NegocioReseña:NegocioGenerico<Reseña>,INegocioReseña
    {

        public NegocioReseña(ILogger logger) : base(logger)
        {


        }
        public Task<List<ReseñaRespuesta>> GetReseñas()
        {
            try { 
            var query = (from reseña in Context.Reseñas
            join producto in Context.Productos
            on reseña.IdProducto equals producto.IdProducto
            join usuario in Context.Usuarios
            on reseña.IdUsuario equals usuario.IdUsuario
            select new ReseñaRespuesta
            {
                IdUsuario= reseña.Id,
                NombreUsuario= usuario.Nombre,
                IdProducto= producto.IdProducto,
                DescripcionProducto=producto.Descripcion,
                IdValoracion= reseña.IdValoracion,
                Comentario=reseña.Comentario,
                Id=reseña.Id


            });

            return (Task<List<ReseñaRespuesta>>)query;
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioReseña" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetReseñas(NegocioReseña)");
            }
        }

    }
}
