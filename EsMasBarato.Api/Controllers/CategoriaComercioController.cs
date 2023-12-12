using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaComercioController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;

        public CategoriaComercioController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaComercioDto>>> GetCategoriaComercio()
        {
            try
            {
                var listaCategoriasComercio = await _unidadDeTrabajo.CategoriasComercio
                    .GetAllAsync(); // Materialize the query

                if (listaCategoriasComercio.Any())
                {
                    List<CategoriaComercioDto> listaRespuesta = _mapper.Map<List<CategoriaComercioDto>>(listaCategoriasComercio);
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Está Lista Para Ser Utilizada",
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

                return StatusCode(500, "Se produjo un error al obtener las categorías de comercio.");
            }
        }



        [HttpPost]
        public async Task<ActionResult> CargarCategoriaComercio([FromBody] CategoriaComercioDto categoriaComercioDto)
        {
            try
            {
                var categoriaComercio = await _unidadDeTrabajo.CategoriasComercio.GetByConditionAsync(c => c.Nombre == categoriaComercioDto.Nombre);

                if (categoriaComercio == null)
                {
                    CategoriaComercio categoriaComercioNew = _mapper.Map<CategoriaComercio>(categoriaComercioDto);

                    await _unidadDeTrabajo.CategoriasComercio.InsertAsync(categoriaComercioNew);
                    await _unidadDeTrabajo.CategoriasComercio.SaveAsync(); // Guardar los cambios asincrónicamente
                    return Ok(new { success = true, message = "La Categoría de comercio fue creada con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "La Categoría de comercio ya existe", result = 409 });
                }
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditCategoriaComercio(CategoriaComercioDto categoriaComercioDto)
        {
            try
            {
                var categoriaComercio = await _unidadDeTrabajo.CategoriasComercio.GetByIdAsync((int)categoriaComercioDto.Id);

                if (categoriaComercio != null)
                {
                    _mapper.Map(categoriaComercioDto, categoriaComercio);
                    await _unidadDeTrabajo.CategoriasComercio.UpdateAsync(categoriaComercio);

                    return Ok(new { success = true, message = "La Categoria del coemrcio fue actualizada", result = 200 });
                }

                return Conflict(new { success = false, message = "La Categoría No Se Encontró", result = 409 });
            }
            catch
            {
                return BadRequest();
            }
        }



    }
}
