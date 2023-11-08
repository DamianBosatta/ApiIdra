using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Entidades.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseñaController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;

        public ReseñaController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseñaDto>>> GetReseñas()
        {
            try
            {
                var listaReseñas = await _unidadDeTrabajo.Reseñas
                    .GetAllAsync(); // Materialize the query

                if (listaReseñas.Any())
                {
                    var listaRespuesta = _mapper.Map<List<ReseñaDto>>(listaReseñas);
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Puede Ser Utilizada",
                        result = listaRespuesta
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Se produjo un error al obtener las categorías.");
            }
        }



        [HttpPost]
        public async Task<ActionResult> CargarReseña([FromBody] ReseñaDto reseñaDto)
        {
            try
            {
                           
               
                   Reseña reseñaNew = _mapper.Map<Reseña>(reseñaDto);

                    await _unidadDeTrabajo.Reseñas.InsertAsync(reseñaNew);
                    await _unidadDeTrabajo.Reseñas.SaveAsync(); // Guardar los cambios asincrónicamente
                    return Ok(new { success = true, message = "La Reseña fue creada con éxito", result = 200 });
               
                   
                
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditReseña(ReseñaDto reseñaDto)
        {
            try
            {
                var reseña = await _unidadDeTrabajo.Reseñas.GetByIdAsync((int)reseñaDto.Id);

                if (reseña != null)
                {
                    _mapper.Map(reseñaDto, reseña);
                    await _unidadDeTrabajo.Reseñas.UpdateAsync(reseña);

                    return Ok(new { success = true, message = "La Reseña fue actualizada", result = 200 });
                }

                return Conflict(new { success = false, message = "La Reseña No Se Encontró", result = 409 });
            }
            catch
            {
                return BadRequest();
            }
        }



    }
}
