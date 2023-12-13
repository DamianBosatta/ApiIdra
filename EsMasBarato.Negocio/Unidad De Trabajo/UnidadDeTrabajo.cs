using EsMasBarato.Negocios.Negocios.NegociosCategoria;
using EsMasBarato.Negocios.Negocios.NegociosCategoriaComercio;
using EsMasBarato.Negocios.Negocios.NegociosComercio;
using EsMasBarato.Negocios.Negocios.NegociosProducto;
using EsMasBarato.Negocios.Negocios.NegociosReseña;
using EsMasBarato.Negocios.Negocios.NegociosRol;
using EsMasBarato.Negocios.Negocios.NegociosUsuario;
using EsMasBarato.Negocios.Negocios.NegociosValoracion;
using Microsoft.Extensions.Logging;

namespace EsMasBarato.Negocios.Unidad_De_Trabajo
{
    public class UnidadDeTrabajo:IUnidadDeTrabajo
    {
        protected readonly ILogger _logger;
        public UnidadDeTrabajo()
        {
           
        }

        private INegocioCategoria? categorias;
        private INegocioCategoriaComercio? categoriasComercio;
        private INegocioComercio? comercios;
        private INegocioProducto? productos;
        private INegocioReseña? reseñas;
        private INegociosRol? roles;
        private INegocioUsuario? usuarios;
        private INegocioValoracion? valoraciones;
        private INegocioValoracionComercio? valoracionesComercio;

        public INegocioCategoria Categorias
        {
            get
            {
                return categorias ?? (categorias = new NegocioCategoria((Serilog.ILogger)_logger));
            }
        }

        public INegocioComercio Comercios
        {
          get { return comercios ?? (comercios = new NegocioComercio((Serilog.ILogger)_logger)); }


        }

        public INegocioCategoriaComercio CategoriasComercio
        {
            get { return categoriasComercio ?? (categoriasComercio = new NegocioCategoriaComercio((Serilog.ILogger)_logger)); }

        }

        public INegocioProducto Productos
        {
            get { return productos ?? (productos = new NegocioProducto((Serilog.ILogger)_logger)); }
        }

        public INegocioReseña Reseñas
        {
            get { return reseñas ?? (reseñas = new NegocioReseña((Serilog.ILogger)_logger)); }
        }

        public INegociosRol Roles
        {
            get { return roles ?? (roles = new NegocioRol((Serilog.ILogger)_logger)); }
        }

        public INegocioUsuario Usuarios
        {
            get { return usuarios ?? (usuarios = new NegocioUsuario((Serilog.ILogger)_logger)); }
        }
         
        public INegocioValoracion Valoraciones
        {
            get { return valoraciones ?? (valoraciones = new NegocioValoracion((Serilog.ILogger)_logger)); }
        }

        public INegocioValoracionComercio ValoracionesComercio
        {
            get { return valoracionesComercio ?? (valoracionesComercio = new NegocioValoracionComercio((Serilog.ILogger)_logger)); }
        }

        public void Dispose()
        {

        }
    }
}
