using EsMasBarato.Negocios.Negocios.NegociosCategoria;
using EsMasBarato.Negocios.Negocios.NegociosCategoriaComercio;
using EsMasBarato.Negocios.Negocios.NegociosComercio;
using EsMasBarato.Negocios.Negocios.NegociosProducto;
using EsMasBarato.Negocios.Negocios.NegociosReseña;
using EsMasBarato.Negocios.Negocios.NegociosRol;
using EsMasBarato.Negocios.Negocios.NegociosUsuario;
using EsMasBarato.Negocios.Negocios.NegociosValoracion;

namespace EsMasBarato.Negocios.Unidad_De_Trabajo
{
    public interface IUnidadDeTrabajo:IDisposable
    {
        INegocioCategoria Categorias { get; }
        INegocioCategoriaComercio CategoriasComercio { get; }
        INegocioComercio Comercios { get; }
        INegocioProducto Productos { get; }
        INegocioReseña Reseñas { get; }
        INegociosRol Roles { get; }
        INegocioUsuario Usuarios { get; }
        INegocioValoracion Valoraciones { get; }
        INegocioValoracionComercio ValoracionesComercio { get; }

    }
}
