using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Api.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;
using EsMasBarato.Entidades.DtoRespuesta;
using Microsoft.AspNetCore.Authorization;

namespace EsMasBarato.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper, ILogger<ProductoController> logger)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoRespuesta>>> GetProductos()
        {
            try
            {
                var listaProductos = await _unidadDeTrabajo.Productos.GetProductos(0, 0);
                    ; // Materialize the query

                if (listaProductos.Any())
                {
                    
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista puede Ser Utilizada",
                        result = listaProductos
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Productos," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetProductos(Controller Productos)");
            }
        }

        [HttpGet("comercio")]
        public async Task<ActionResult<IEnumerable<ProductoRespuesta>>> GetProductosPorComercio([FromQuery] int idComercio)
        {
            try
            {
                var listaProductos = await _unidadDeTrabajo.Productos.GetProductos(idComercio, 0); // Materialize the query

                if (listaProductos.Any())
                {
                    
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista puede Ser Utilizada",
                        result = listaProductos
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Productos," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetProductosPorComercio(Controller Productos)");
            }
        }

        [HttpGet("categoria")]
        public async Task<ActionResult<IEnumerable<ProductoRespuesta>>> GetProductosPorCategoria([FromQuery] int idCategoria)
        {
            try
            {
                var listaProductos = await _unidadDeTrabajo.Productos.GetProductos(0, idCategoria);
                // Materialize the query

                if (listaProductos.Any())
                {
                   
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista puede Ser Utilizada",
                        result = listaProductos
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Productos," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetProductosPorCategoria(Controller Productos)");
            }
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProductoRespuesta>>> GetProductosPorCondicion([FromQuery] string termino)
        {
            try
            {
                var listaProductos = await _unidadDeTrabajo.Productos.GetProductosPorCondicion(termino);
                // Materialize the query

                if (listaProductos.Any())
                {

                    return Ok(new
                    {
                        success = true,
                        message = "La Lista puede Ser Utilizada",
                        result = listaProductos
                    });
                }
                else
                {
                    return NoContent(); // 204 No Content is more appropriate for empty results
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Productos," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetProductosPorCategoria(Controller Productos)");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> CargarProducto([FromBody] ProductoDto productoDto)
        {
            try
            {
                var producto = await _unidadDeTrabajo.Productos.GetByConditionAsync(c => c.CodigoBarra == productoDto.CodigoBarra &&
                c.IdComercio == productoDto.IdComercio);

                if (producto == null)
                {
                    Producto productoNew = _mapper.Map<Producto>(productoDto);

                    await _unidadDeTrabajo.Productos.InsertAsync(productoNew);
                   
                    return Ok(new { success = true, message = "El producto fue creado con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "El producto ya existe", statusCode = 409, producto });
                }
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Productos," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En CargarProducto(Controller Productos)");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditProducto(ProductoDto productoDto)
        {
            try
            {
                var producto = await _unidadDeTrabajo.Productos.GetByIdAsync(productoDto.IdProducto);

                if (producto != null)
                {
                    _mapper.Map(productoDto, producto);
                    await _unidadDeTrabajo.Productos.UpdateAsync(producto);

                    return Ok(new { success = true, message = "El producto fue actualizado", result = 200 });
                }

                return Conflict(new { success = false, message = "El producto No Se Encontró", result = 409 });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Productos," +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En EditProducto(Controller Productos)");
            }
        }



    }
}
