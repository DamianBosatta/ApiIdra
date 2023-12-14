using AutoMapper;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.Codigos_Utiles;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValoracionComercioController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<ValoracionComercioController> _logger;

        public ValoracionComercioController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper, ILogger<ValoracionComercioController> logger)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
            _logger = logger;
        }

        [Route("Valoracion/")]
        [HttpGet("{idValoracion}")]
        public async Task<ActionResult<IEnumerable<ValoracionComercioRespuesta>>> GetValoracionComercioporValoracion(int idValoracion)
        {
            try
            {
                var listaValoracionComercio = await _unidadDeTrabajo.ValoracionesComercio.
                GetValoracionComercios(CodigosUtiles.OpcionTodos,idValoracion);

                if (listaValoracionComercio.Any())
                {
                    
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Está Lista Para Ser Utilizada",
                        result = listaValoracionComercio
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De ValoracionComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetValoracionComercio(Controller ValoracionComercio)");
            }
        }
        
        [Route("Comercio/")]
        [HttpGet("{idComercio}")]
        public async Task<ActionResult<IEnumerable<ValoracionComercioRespuesta>>> GetValoracionPorComercio(int idComercio)
        {
            try
            {
                var listaValoracionComercio = await _unidadDeTrabajo.ValoracionesComercio.
                    GetValoracionComercios(idComercio,CodigosUtiles.OpcionTodos);

                if (listaValoracionComercio.Any())
                {

                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Está Lista Para Ser Utilizada",
                        result = listaValoracionComercio
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De ValoracionComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetValoracionComercio(Controller ValoracionComercio)");
            }
        }



        [HttpPost]
        public async Task<ActionResult> CargarValoracionComercio([FromBody] ValoracionComercioDto ValoracionComercioDto)
        {
            try
            {
                var ValoracionComercio = await _unidadDeTrabajo.ValoracionesComercio.GetByIdAsync(ValoracionComercioDto.Id);

                if (ValoracionComercio == null)
                {
                    ValoracionComercio ValoracionComercioNew = _mapper.Map<ValoracionComercio>(ValoracionComercioDto);

                    await _unidadDeTrabajo.ValoracionesComercio.InsertAsync(ValoracionComercioNew);
                   
                    return Ok(new { success = true, message = "La Valoracion del comercio fue creada con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "La Valoracion Comercio ya existe", result = 409 });
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De ValoracionComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CargarValoracionComercio(Controller ValoracionComercio)");
            }
        
    }

        [HttpPut]
        public async Task<IActionResult> EditValoracionComercio(ValoracionComercioDto ValoracionComercioDto)
        {
            try
            {
                var valoracionComercio = await _unidadDeTrabajo.ValoracionesComercio.GetByIdAsync(ValoracionComercioDto.Id);

                if (valoracionComercio != null)
                {
                    _mapper.Map(ValoracionComercioDto, valoracionComercio);
                    await _unidadDeTrabajo.ValoracionesComercio.UpdateAsync(valoracionComercio);

                    return Ok(new { success = true, message = "La Valoracion Comercio fue actualizada", result = 200 });
                }

                return Conflict(new { success = false, message = "La Valoracion Comercio No Se Encontró", result = 409 });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De ValoracionComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En EditValoracionComercio(Controller ValoracionComercio)");
            }
        }

    }
}
