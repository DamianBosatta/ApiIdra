using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using EsMasBarato.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioUsuario _servicio;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUnidadDeTrabajo uow, IMapper mapper, IServicioUsuario servicio, ILogger<UsuarioController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _servicio = servicio;
            _logger = logger;
        }

       
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioRespuesta>>> GetUsuarios()
        {
            try
            {
                var ListaUsuarios = await _uow.Usuarios.GetUsuarios();

                if (ListaUsuarios.Any())
                {
                    List<UsuarioRespuesta> listaUsuarioRespuesta = _mapper.Map<List<UsuarioRespuesta>>(ListaUsuarios);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaUsuarioRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<UsuarioRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Usuario" +
                " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetUsuarios(Controller Usuario)");
            }
        }

        [Route("Registro/")]
        [HttpPost]
        public async Task<ActionResult> RegistrarUsuarioAsync([FromBody] UsuarioDto usuarioReq)
        {
            try
            {
                var usuarioExiste = await _uow.Usuarios.GetByConditionAsync(c => c.Email == usuarioReq.Email &&  c.Borrado == 0);

                if (usuarioExiste is null)
                {
                    var usuario = _servicio.RegistroUsuario(usuarioReq);
                   await _uow.Usuarios.InsertAsync(usuario);
                    UsuarioRespuesta usuarioRes = new();
                    _mapper.Map(usuario, usuarioRes);
                    return Ok(new { success = true, message = "El Usuario fue Creado Con Exito", result = usuarioRes });
                }

                return Ok(new { success = false, message = " El Usuario Ya Esta Registrado ", result = new UsuarioRespuesta() });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Usuario" +
                " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En RegistrarUsuario(Controller Usuario)");
            }
        }
        [Route("Login/")]
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto login)
        {
            try
            {
                DateTime expiraToken = DateTime.Now.AddHours(8);
                var usuario = await _uow.Usuarios.GetByConditionAsync(u => u.Email == login.Email && u.Borrado == 0);
                var loginRespuesta = new LoginRespuesta();

                if (usuario != null)
                {
                    if (!_servicio.VerificarPassworHash(login.Password, usuario.PaswordHash, usuario.PasswordSalt))
                    {
                        loginRespuesta.Token = "Password Incorrecto";
                        return Ok(new { success = false, message = "Password Incorrecto", result = loginRespuesta.Token });
                    }

                    string token = _servicio.CreateToken(usuario, expiraToken);

            
                    loginRespuesta.Nombre = usuario.Nombre;
                    loginRespuesta.Email = usuario.Email;
                    loginRespuesta.Id = usuario.IdUsuario;
                    loginRespuesta.IdRol = usuario.IdRol;
                    loginRespuesta.Token = token;
                 

                    return Ok(new { success = true, message = "Login Correcto", result = loginRespuesta });
                }

                loginRespuesta.Token = "No Se Encontro El Usuario";
                return Ok(new { success = false, message = "No Se Encontro El Usuario", result = loginRespuesta.Token });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Usuario" +
                " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En Login(Controller Usuario)");
            }
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> PasswordCambio(int id, string passwordNew)
        {
            try
            {
                var usuario = await _uow.Usuarios.GetByIdAsync(id);

                if (usuario != null && usuario.Borrado == 0)
                {

                    var user =  _servicio.CambiarPassword(usuario, passwordNew);


                    if (user.PaswordHash is null && user.PasswordSalt is null)
                    {
                        return Ok(new { success = false, message = "No se puede poner el mismo password existente", result = new UsuarioRespuesta() == null });
                    }
                    else
                    {
                       await _uow.Usuarios.UpdateAsync(user);
                        UsuarioRespuesta usuarioRes = new();
                        _mapper.Map(user, usuarioRes);
                        return Ok(new { success = true, message = "El Password Fue Actualizado", result = usuarioRes });

                    }

                }

                return Ok(new { success = false, message = "No se Encontro El Usuario", result = new UsuarioRespuesta() == null });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Usuario" +
                " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En PasswordCambio(Controller Usuario)");
            }
        }

        [Route("Borrar/{id?}"), Authorize]
        [HttpPut]
        public async Task<IActionResult> IsDeletedUsuarioAsync(int id)
        {
            try
            {
                var usuario = await _uow.Usuarios.GetByIdAsync(id);
                if (usuario != null && usuario.Borrado == 0)
                {
                    usuario.Borrado = 1;
                    await _uow.Usuarios.UpdateAsync(usuario);
                    UsuarioRespuesta usuarioRes = new();
                    _mapper.Map(usuario, usuarioRes);

                    return Ok(new { success = true, message = "El Usuario Fue Eliminado ", result = usuarioRes });


                }


                return Ok(new { success = false, message = "El Usuario No Existe ", result = new UsuarioRespuesta() == null });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Usuario" +
                " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En IsDeletedUsuario(Controller Usuario)");
            }
        }


    }
}
