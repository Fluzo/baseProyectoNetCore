using AutoMapper;
using BD_Prueba.Entidades;
using BD_Prueba.DTOs;

namespace BD_Prueba.DTOs
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Articulo, CrearArticuloDTO>()
                //.ForMember(x => x.PedidosArticulos, o => o.Ignore())
                .ReverseMap();

            //CreateMap<Estado, EstadoDTO>()
            //    .ForMember(x => x.Pedidos, o => o.Ignore())
            //    .ReverseMap();

            CreateMap<PedidosArticulo, PedidoArticuloDTO>()
                .ReverseMap();

            //CreateMap<Pedido, PedidoDTO>()
            //    .ForMember(x => x.PedidosArticulos, o => o.Ignore())
            //    .ReverseMap();

            CreateMap<Perfil, PerfileDTO>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioDTO>()
                //.ForMember(x => x.Pedidos, o => o.Ignore())
                //.ForMember(x => x.Perfiles, o => o.Ignore())
                .ReverseMap();



        } 
    }
}
