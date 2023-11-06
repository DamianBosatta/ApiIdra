using AutoMapper;
using EsMasBarato.Entidades.Dto;
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

                var listaCategorias = _unidadDeTrabajo.Categorias.GetAllByCondition(u => u.Borrado == 0).ToList();

                if (listaCategorias.Count() > 0)
                {
                    List<CategoriaDto> listaRespuesta = _mapper.Map<List<CategoriaDto>>(listaCategorias);

                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaRespuesta });

                }
                else
                {
                    return Ok(new { success = false, message = "La Lista No Contiene Datos", result = 204 });
                }

            }
            catch
            {

                return BadRequest();
            }

        }

    }
}
