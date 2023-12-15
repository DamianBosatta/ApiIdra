using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using Microsoft.EntityFrameworkCore;
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
        public Task<List<ReseñaRespuesta>> GetReseñas(int idProducto,int idValoracion,int idUsuario)
        {
            try { 
            var query = (from reseña in Context.Reseñas
            join producto in Context.Productos
            on reseña.IdProducto equals producto.IdProducto
            join usuario in Context.Usuarios
            on reseña.IdUsuario equals usuario.IdUsuario
            select new ReseñaRespuesta
            {
                Id = reseña.Id,
                IdUsuario = reseña.Id,
                NombreUsuario= usuario.Nombre,
                IdProducto= producto.IdProducto,
                DescripcionProducto=producto.Descripcion,
                IdValoracion= reseña.IdValoracion,
                Comentario=reseña.Comentario
                


            });

                query = idProducto == 0 ? query : query.Where(reseña => reseña.IdProducto == idProducto);

                query= idValoracion==0 ? query : query.Where(reseña=> reseña.IdValoracion== idValoracion);

                query = idUsuario== 0? query : query.Where(reseña => reseña.IdUsuario == idUsuario);

                return query.ToListAsync();

            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioReseña" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetReseñas(NegocioReseña)");
            }
        }

        public async Task<decimal?> ObtenerPromedioValoracionProducto(int idProducto)
        {
            try
            {
                var valoracionesProducto = Context.Reseñas
                    .Where(reseña => reseña.IdProducto == idProducto)
                    .Select(valoracion => valoracion.IdValoracion);

                if (valoracionesProducto.Any())
                {
                    return (decimal)valoracionesProducto.Average();
                }
                else
                {
                    return null; // Devuelve null si no hay valoraciones
                }
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En  NegocioReseña" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En ObtenerPromedioValoracionProducto( NegocioReseña)");
            }
        }

    }
}
