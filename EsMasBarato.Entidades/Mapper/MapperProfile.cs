using AutoMapper;
using EsMasBarato.Entidades.Dto;
using EsMasBarato.Entidades.Modelos;

namespace EsMasBarato.Entidades.Mapper
{
    public class MapperProfile:Profile
    {

        public MapperProfile()
        {
            


         
            CreateMap<CategoriaComercioDto, CategoriaComercio>();
            
            CreateMap<CategoriaComercio, CategoriaComercioDto>();
           


            CreateMap<ComercioDto, Comercio>().ReverseMap();

            CreateMap<ProductoDto,Producto>().ReverseMap();

            CreateMap<ReseñaDto,Reseña>().ReverseMap(); 

            CreateMap<UsuarioDto,Usuario>().ReverseMap();   

            CreateMap<ValoracionDto,Valoracion>().ReverseMap();

            CreateMap<ValoracionComercioDto, ValoracionComercio>().ReverseMap();

        }


    }
}
