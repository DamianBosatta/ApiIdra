
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
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

            return await (Task<List<ComercioRespuesta>>)query;
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioComercio" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetComercios(NegocioComercio)");
            }
        }
    }
}
