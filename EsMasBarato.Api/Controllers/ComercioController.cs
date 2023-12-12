using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;
using System;
using EsMasBarato.Entidades.DtoRespuesta;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComercioController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;

        public ComercioController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
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
            catch (Exception ex)
            {

                return StatusCode(500, "Se produjo un error al obtener los comercios");
            }
        }
        [HttpGet("{idComercio}")]
        public async Task<IActionResult> GetComercioById(int idComercio)
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
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { success = false, message = "Ocurrió un error al procesar la solicitud", error = ex.Message });
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
                    await _unidadDeTrabajo.Comercios.SaveAsync(); // Guardar los cambios asincrónicamente
                    return Ok(new { success = true, message = "El comercio fue creado con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "El comercio ya existe", result = 409 });
                }
            }
            catch
            {
                return BadRequest();
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
            catch
            {
                return BadRequest();
            }
        }



    }
}
