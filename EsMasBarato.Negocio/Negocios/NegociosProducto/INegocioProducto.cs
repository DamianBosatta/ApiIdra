
using EsMasBarato.Api.Modelos;
using EsMasBarato.Entidades.DtoRespuesta;
using EsMasBarato.Negocios.NegociosGenericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Negocios.Negocios.NegociosProducto
{
    public interface INegocioProducto:INegocioGenerico<Producto>
    {
        public Task<List<ProductoRespuesta>> GetProductos(int idComercio, int idCategoria);
        public Task<List<ProductoRespuesta>> GetProductosPorCondicion(string termino);
    }
}
