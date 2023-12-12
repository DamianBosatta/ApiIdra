using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;

        public CategoriaController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategoria()
        {
            try
            {
                var listaCategorias = await _unidadDeTrabajo.Categorias
                    .GetAllByConditionAsync(u => u.Borrado == 0); // Materialize the query

                if (listaCategorias.Any())
                {
                    var listaRespuesta = _mapper.Map<List<CategoriaDto>>(listaCategorias);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", 
                    result = listaRespuesta });
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
        public async Task<ActionResult> CargarCategoria([FromBody] CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = await _unidadDeTrabajo.Categorias.GetByConditionAsync(c => c.Descripcion == categoriaDto.Descripcion);

                if (categoria == null)
                {
                    Categoria categoriaNew = _mapper.Map<Categoria>(categoriaDto);

                    await _unidadDeTrabajo.Categorias.InsertAsync(categoriaNew);
                    await _unidadDeTrabajo.Categorias.SaveAsync(); // Guardar los cambios asincrónicamente
                    return Ok(new { success = true, message = "La Categoría fue creada con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "La Categoría ya existe", result = 409 });
                }
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditCategoria(CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = await _unidadDeTrabajo.Categorias.GetByIdAsync((int)categoriaDto.IdCategoria);

                if (categoria != null && categoria.Borrado == 0)
                {
                    _mapper.Map(categoriaDto, categoria);
                    await _unidadDeTrabajo.Categorias.UpdateAsync(categoria);

                    return Ok(new { success = true, message = "La Categoria fue actualizada", result = 200 });
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
