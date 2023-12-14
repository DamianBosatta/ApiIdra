using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;
using System;
using EsMasBarato.Entidades.DtoRespuesta;
using Microsoft.AspNetCore.Authorization;

namespace EsMasBarato.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComercioController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<ComercioController> _logger;

        public ComercioController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper, ILogger<ComercioController> logger)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComercioRespuesta>>> GetComercios()
        {
            try
            {
                var listaComercios = await _unidadDeTrabajo.Comercios.GetComercios(0); // Materialize the query

                if (listaComercios.Any())
                {
                    
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista puede Para Ser Utilizada",
                        result = listaComercios
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Comercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetComercios(Controller Comercio)");
            }
        }
        [HttpGet("{idComercio}")]
        public async Task<ActionResult<IEnumerable<ComercioRespuesta>>> GetComercioById(int idComercio)
        {
            try
            {
                var comercio = await _unidadDeTrabajo.Comercios.GetComercios(idComercio);

                if (comercio != null)
                {
                    
                    return Ok(new { success = true, message = "Response Confirmado", result = comercio });
                }

                return NotFound(new { success = false, message = "No Se Encontro el comercio", result = 204 });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Comercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetComercioById(Controller Comercio)");
            }
        }

        

                [HttpPost]
        public async Task<ActionResult> CargarComercio([FromBody] ComercioDto comercioDto)
        {
            try
            {
                var comercio = await _unidadDeTrabajo.Comercios.GetByConditionAsync(c => c.Nombre == comercioDto.Nombre);

                if (comercio == null)
                {
                    Comercio comercioNew = _mapper.Map<Comercio>(comercioDto);

                    await _unidadDeTrabajo.Comercios.InsertAsync(comercioNew);
                    
                    return Ok(new { success = true, message = "El comercio fue creado con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "El comercio ya existe", result = 409 });
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Comercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CargarComercio(Controller Comercio)");
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditComercio(ComercioDto comercioDto)
        {
            try
            {
                var comercio = await _unidadDeTrabajo.Comercios.GetByIdAsync((int)comercioDto.IdComercio);

                if (comercio != null)
                {
                    _mapper.Map(comercioDto, comercio);
                    await _unidadDeTrabajo.Comercios.UpdateAsync(comercio);

                    return Ok(new { success = true, message = "El comercio fue actualizado", result = 200 });
                }

                return Conflict(new { success = false, message = "El comercio  No Se Encontró", result = 409 });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Comercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En  EditComercio(Controller Comercio)");
            }
        }



    }
}
