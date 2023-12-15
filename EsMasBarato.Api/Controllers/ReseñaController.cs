using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EsMasBarato.Entidades.Codigos_Utiles;
using EsMasBarato.Entidades.DtoRespuesta;

namespace EsMasBarato.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReseñaController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<ReseñaController> _logger;

        public ReseñaController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper, ILogger<ReseñaController> logger)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
            _logger = logger;
        }

        [Route("usuario/{idUser}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseñaRespuesta>>> GetReseñasUsuario(int idUser)
        {
            try
            {
                var listaReseñas = await _unidadDeTrabajo.Reseñas
                    .GetReseñas(CodigosUtiles.OpcionTodos, CodigosUtiles.OpcionTodos, idUser); // Materialize the query

                if (listaReseñas.Any())
                {
                   
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Puede Ser Utilizada",
                        result = listaReseñas
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Reseñas," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En  GetReseñasUsuario(Controller Reseñas)");
            }
        }
        [Route("producto/{idProducto}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseñaRespuesta>>> GetReseñasPorProducto(int idProducto)
        {
            try
            {
                var listaReseñas = await _unidadDeTrabajo.Reseñas
                    .GetReseñas(idProducto, CodigosUtiles.OpcionTodos, CodigosUtiles.OpcionTodos); // Materialize the query

                if (listaReseñas.Any())
                {

                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Puede Ser Utilizada",
                        result = listaReseñas
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Reseñas," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetReseñasPorProducto(Controller Reseñas)");
            }
        }

        [Route("valoracion/{idValoracion}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReseñaRespuesta>>> GetReseñasPorValoracion(int idValoracion)
        {
            try
            {
                var listaReseñas = await _unidadDeTrabajo.Reseñas
                    .GetReseñas(CodigosUtiles.OpcionTodos, idValoracion, CodigosUtiles.OpcionTodos); // Materialize the query

                if (listaReseñas.Any())
                {

                    return Ok(new
                    {
                        success = true,
                        message = "La Lista Puede Ser Utilizada",
                        result = listaReseñas
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Reseñas," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En  GetReseñasPorValoracion(Controller Reseñas)");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CargarReseña([FromBody] ReseñaDto reseñaDto)
        {
            try
            {
                           
               
                   Reseña reseñaNew = _mapper.Map<Reseña>(reseñaDto);

                    await _unidadDeTrabajo.Reseñas.InsertAsync(reseñaNew);
                    
                    return Ok(new { success = true, message = "La Reseña fue creada con éxito", result = 200 });
               
                   
                
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Reseñas," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CargarReseña(Controller Reseñas)");
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
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Reseñas," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En EditReseña(Controller Reseñas)");
            }
        }


    }
}
