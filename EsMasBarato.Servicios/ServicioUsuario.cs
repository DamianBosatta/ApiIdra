using AutoMapper;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.Codigos_Utiles;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EsMasBarato.Servicios
{
    public class ServicioUsuario:IServicioUsuario
    {



        private readonly IUnidadDeTrabajo _uow;
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<ServicioUsuario> _logger;



        public ServicioUsuario(IConfiguration configuration, IUnidadDeTrabajo uow, IMapper mapper, ILogger<ServicioUsuario> logger)
        {
            _configuration = configuration;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        #region REGISTRO DE USUARIO
        public Usuario RegistroUsuario(UsuarioDto userReq)
        {
            try
            {
                Usuario user = new Usuario();

                CreatePasswordhHash(userReq.Password, out byte[] passwordHash, out byte[] passwordSalt);
                             
                user.Nombre = userReq.Nombre;
                user.IdRol = userReq.IdRol;
                user.Email = userReq.Email;
                user.PaswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                return user;
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En ServicioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En  RegistroUsuario(ServicioUsuario)");
            }
        }
        #endregion

        #region CAMBIAR CONTRASEÑA
        public Usuario CambiarPassword(Usuario usuario, string password)
        {
            try
            {
                if (VerificarPassworHash(password, usuario.PaswordHash, usuario.PasswordSalt))
                {
                    // El password proporcionado es el mismo que el actual, no se realiza ningún cambio
                    return new Usuario();
                }

                CreatePasswordhHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                usuario.PaswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;

                return usuario;
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En ServicioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CambiarPassword(ServicioUsuario)");
            }
        }

        #endregion

        #region CREAR TOKEN
        public string CreateToken(Usuario user, DateTime expiraToken)
        {
            try
            {
                List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim(ClaimTypes.Role, ConvertirRolString((int)user.IdRol)),
        };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expiraToken,
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En ServicioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CreateToken(ServicioUsuario)");
            }
        }

        #endregion

        #region CREAR HASH DE CONTRASEÑA
        public void CreatePasswordhHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En ServicioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CreatePasswordhHash(ServicioUsuario)");
            }
        }
        #endregion

        #region VERIFICAR HASH DE CONTRASEÑA
        public bool VerificarPassworHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En ServicioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En VerificarPassworHash(ServicioUsuario)");
            }
        }
        #endregion

        #region CONVERTIR ROL A CADENA
        public string ConvertirRolString(int idRol)
        {
            try
            {
                var rol = idRol == CodigosUtiles.Administrador ? CodigosUtiles.AdministradorNombre : CodigosUtiles.UsuarioNombre;
                return rol;
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En ServicioUsuario" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En ConvertirRolString(ServicioUsuario)");
            }
        }
        #endregion



    }
    public interface IServicioUsuario
    {
        public Usuario RegistroUsuario(UsuarioDto userReq);
        public Usuario CambiarPassword(Usuario usuario, string password);
        public string CreateToken(Usuario user, DateTime expiraToken);
        public void CreatePasswordhHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerificarPassworHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string ConvertirRolString(int idRol);
    }
}