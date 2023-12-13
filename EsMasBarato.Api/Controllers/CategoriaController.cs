using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EsMasBarato.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper, ILogger<CategoriaController> logger)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
            _logger = logger;
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
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Categoria," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetCategoria(Controller Categoria)");
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
                    
                    return Ok(new { success = true, message = "La Categoría fue creada con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "La Categoría ya existe", result = 409 });
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Categoria," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En  CargarCategoria(Controller Categoria)");
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
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Categoria," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En EditCategoria(Controller Categoria)");
            }
        }



    }
}
