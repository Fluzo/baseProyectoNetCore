using System;
using System.Collections.Generic;

namespace BD_Prueba.Entidades
{
    public partial class PedidosArticulo
    {
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }

        //public virtual Articulo IdArticuloNavigation { get; set; } = null!;
        //public virtual Pedido IdPedidoNavigation { get; set; } = null!;
    }
}
