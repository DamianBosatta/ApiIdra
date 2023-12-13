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
    public class CategoriaComercioController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriaComercioController> _logger;

        public CategoriaComercioController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper, ILogger<CategoriaComercioController> logger)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
            _logger = logger;
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
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De CategoriaComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetCategoriaComercio(Controller CategoriaComercio)");
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
                   
                    return Ok(new { success = true, message = "La Categoría de comercio fue creada con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "La Categoría de comercio ya existe", result = 409 });
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De CategoriaComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CargarCategoriaComercio(Controller CategoriaComercio)");
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
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De CategoriaComercio," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En EditCategoriaComercio(Controller CategoriaComercio)");
            }
        }



    }
}
