using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Entidades.Modelos;
using EsMasBarato.Negocios.Unidad_De_Trabajo;
using Microsoft.AspNetCore.Mvc;

namespace EsMasBarato.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMapper _mapper;

        public ProductoController(IUnidadDeTrabajo unidadDeTrabajo, IMapper mapper)
        {

            _mapper = mapper;
            _unidadDeTrabajo = unidadDeTrabajo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            try
            {
                var listaProductos = await _unidadDeTrabajo.Productos
                    .GetAllAsync(); // Materialize the query

                if (listaProductos.Any())
                {
                    var listaRespuesta = _mapper.Map<List<ProductoDto>>(listaProductos);
                    return Ok(new
                    {
                        success = true,
                        message = "La Lista puede Ser Utilizada",
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
        public async Task<ActionResult> CargarProducto([FromBody] ProductoDto productoDto)
        {
            try
            {
                var producto = await _unidadDeTrabajo.Productos.GetByConditionAsync(c => c.CodigoBarra == productoDto.CodigoBarra);

                if (producto == null)
                {
                    Producto productoNew = _mapper.Map<Producto>(productoDto);

                    await _unidadDeTrabajo.Productos.InsertAsync(productoNew);
                    await _unidadDeTrabajo.Productos.SaveAsync(); // Guardar los cambios asincrónicamente
                    return Ok(new { success = true, message = "El producto fue creado con éxito", result = 200 });
                }
                else
                {
                    return Conflict(new { success = false, message = "El producto ya existe", result = 409 });
                }
            }
            catch
            {
                return BadRequest();
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
            catch
            {
                return BadRequest();
            }
        }



    }
}
