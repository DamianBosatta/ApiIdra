
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using Microsoft.EntityFrameworkCore;

namespace EsMasBarato.Negocios.Negocios.NegociosProducto
{
    public class NegocioProducto:NegocioGenerico<Producto>,INegocioProducto
    {
        public NegocioProducto() : base()
        {


        }

        public async Task<List<ProductoRespuesta>> GetProductos(int idComercio, int idCategoria)
        {
            var query = (from producto in Context.Productos
                         join comercio in Context.Comercios
                         on producto.IdComercio equals comercio.IdComercio
                         join categoria in Context.Categorias
                         on producto.IdCategoria equals categoria.IdCategoria
                         select new ProductoRespuesta
                         {
                             IdProducto = producto.IdProducto,
                             Descripcion = producto.Descripcion,
                             PrecioRegular = producto.PrecioRegular,
                             PrecioWeb = producto.PrecioWeb,
                             IdComercio = producto.IdComercio,
                             DescripcionComercio = comercio.Nombre,
                             IdCategoria = producto.IdCategoria,
                             DescripcionCategoria = categoria.Descripcion,
                             CodigoBarra = producto.CodigoBarra,
                             Activo = producto.Activo,
                             Anunciado = producto.Anunciado,
                             Valoracion = producto.Valoracion

                         });


            query = idComercio != 0 ? query.Where(producto => producto.IdComercio == idComercio) : query;
            query = idCategoria != 0 ? query.Where(producto => producto.IdCategoria == idCategoria) : query;

            return await query.ToListAsync();

        }
    }
}
