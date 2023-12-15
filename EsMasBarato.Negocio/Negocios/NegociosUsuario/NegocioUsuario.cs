using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EsMasBarato.Negocios.Negocios.NegociosUsuario
{
  public class NegocioUsuario:NegocioGenerico<Usuario>,INegocioUsuario
    {
        public NegocioUsuario(ILogger logger) : base(logger)
        {


        }

        public async Task<List<UsuarioRespuesta>> GetUsuarios(int idUser)
        {
            try {
                var query = (from usuario in Context.Usuarios.Where(u => u.Borrado == 0)
                             join rol in Context.Rols on usuario.IdRol equals rol.Id
                             join comercio in Context.Comercios on usuario.IdComercio equals comercio.IdComercio into comercioJoin
                             from comercio in comercioJoin.DefaultIfEmpty()
                             select new UsuarioRespuesta
                             {
                                 Id = usuario.IdUsuario,
                                 Nombre = usuario.Nombre,
                                 Email = usuario.Email,
                                 IdRol = usuario.IdRol,
                                 TipoRol = rol.TipoRol,
                                 IdComercio = comercio != null ? comercio.IdComercio : -1,
                                 TipoComercio = comercio != null ? comercio.Nombre : null
                             });

                query = idUser != 0 ? query.Where(usuario => usuario.Id == idUser) : query;

            return await query.ToListAsync();
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetUsuarios(NegocioUsuario)");
            }

        }
    }
}
