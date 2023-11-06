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
    public class UnidadDeTrabajo:IUnidadDeTrabajo
    {
        public UnidadDeTrabajo()
        {

        }

        private INegocioCategoria categorias;
        private INegocioCategoriaComercio categoriasComercio;
        private INegocioComercio comercios;
        private INegocioProducto productos;
        private INegocioReseña reseñas;
        private INegociosRol roles;
        private INegocioUsuario usuarios;
        private INegocioValoracion valoraciones;
        private INegocioValoracionComercio valoracionesComercio;

        public INegocioCategoria Categorias
        {
            get
            {
                return categorias ?? (categorias = new NegocioCategoria());
            }
        }

        public INegocioComercio Comercios
        {
          get { return comercios ?? (comercios = new NegocioComercio()); }


        }

        public INegocioCategoriaComercio CategoriasComercio
        {
            get { return categoriasComercio ?? (categoriasComercio = new NegocioCategoriaComercio()); }

        }

        public INegocioProducto Productos
        {
            get { return productos ?? (productos = new NegocioProducto()); }
        }

        public INegocioReseña Reseñas
        {
            get { return reseñas ?? (reseñas = new NegocioReseña()); }
        }

        public INegociosRol Roles
        {
            get { return roles ?? (roles = new NegocioRol()); }
        }

        public INegocioUsuario Usuarios
        {
            get { return usuarios ?? (usuarios = new NegocioUsuario()); }
        }
         
        public INegocioValoracion Valoraciones
        {
            get { return valoraciones ?? (valoraciones = new NegocioValoracion()); }
        }

        public INegocioValoracionComercio ValoracionesComercio
        {
            get { return valoracionesComercio ?? (valoracionesComercio = new NegocioValoracionComercio()); }
        }

        public void Dispose()
        {

        }
    }
}
