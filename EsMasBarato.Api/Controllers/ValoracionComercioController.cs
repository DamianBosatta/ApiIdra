using AutoMapper;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValoracionComercioController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;

        public ValoracionComercioController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValoracionComercioRespuesta>>> GetValoracionComercio()
        {
            try
            {
                var listaValoracionComercio = await _unidadDeTrabajo.ValoracionesComercio.GetValoracionComercios();

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
            catch (Exception ex)
            {

                return StatusCode(500, "Se produjo un error al obtener las Valoraciones del Comercio.");
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
                    await _unidadDeTrabajo.ValoracionesComercio.SaveAsync(); // Guardar los cambios asincrónicamente
                    return Ok(new { success = true, message = "La Valoracion del comercio fue creada con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "La Valoracion Comercio ya existe", result = 409 });
                }
            }
            catch
            {
                return BadRequest();
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
            catch
            {
                return BadRequest();
            }
        }

    }
}
