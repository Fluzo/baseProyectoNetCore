using BD_Prueba.DTOs;

namespace BD_Prueba.Entidades
{
    public class PedidoArticuloDTO
    {
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }

        public virtual CrearArticuloDTO IdArticuloNavigation { get; set; } = null!;
        public virtual PedidoDTO IdPedidoNavigation { get; set; } = null!;
    }
}
