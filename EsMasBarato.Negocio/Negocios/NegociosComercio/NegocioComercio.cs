
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace EsMasBarato.Negocios.Negocios.NegociosComercio
{
    public class NegocioComercio:NegocioGenerico<Comercio>,INegocioComercio
    {
        public NegocioComercio(ILogger logger) : base(logger)
        {


        }
        public async Task<List<ComercioRespuesta>> GetComercios(int idComercio)
        {
            try { 
            var query = (from comercio in Context.Comercios
                        join categoria in Context.Categorias
                        on comercio.IdCategoria equals categoria.IdCategoria
                        join rol in Context.Rols
                        on comercio.IdRol equals rol.Id
                        select new ComercioRespuesta
                        {
                            IdComercio= comercio.IdComercio,
                            Nombre=comercio.Nombre,
                            Direccion=comercio.Direccion,
                            IdCategoria=comercio.IdCategoria,
                            DescripcionCategoria= categoria.Descripcion,
                            NombreContacto= comercio.NombreContacto,
                            NumeroTelefono= comercio.NumeroTelefono,
                            IdRol=comercio.IdRol,
                            DescripcionRol= rol.TipoRol,
                            Valoracion=comercio.Valoracion,

                        });

            query= idComercio!=0 ? query.Where(c=>c.IdComercio==idComercio) : query;

            return await query.ToListAsync();
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioComercio" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetComercios(NegocioComercio)");
            }
        }

        public async Task<decimal> ObtenerPromedioValoracionComercio(int idComercio)
        {
            try
            {
                var valoracionesComercio =  Context.ValoracionComercios
                    .Where(valComercio => valComercio.IdComercio == idComercio)
                    .Select(valComercio => valComercio.IdValoracion)
                    .ToList();

                if (valoracionesComercio.Count > 0)
                {
                    decimal promedioValoracion = (decimal)valoracionesComercio.Average();
                    return promedioValoracion;
                }
                else
                {
                   
                    return 0; // O cualquier otro valor por defecto
                }
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioComercio" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En ObtenerPromedioValoracionComercio(NegocioComercio)");
            }
        }

    }
}
