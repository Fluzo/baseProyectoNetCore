using System;
using System.Collections.Generic;

namespace BD_Prueba.Entidades
{
    public partial class Pedido
    {
        //public Pedido()
        //{
        //    PedidosArticulos = new HashSet<PedidosArticulo>();
        //}

        public int ID_PEDIDO { get; set; }
        public string COD_PEDIDO { get; set; }
        public string PISO { get; set; }
        public string PUERTA { get; set; }
        public string CODIGO_POSTAL { get; set; }
        public string NOMBRE_CLIENTE { get; set; }

        //public DateTime Fecha { get; set; }
        public int ID_ESTADO { get; set; }
        public decimal MONTANTE { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }

        //public virtual Estado IdEstadoNavigation { get; set; } = null!;
        //public virtual Usuarios IdUsuarioNavigation { get; set; } = null!;
        //public virtual ICollection<PedidosArticulo> PedidosArticulos { get; set; }
    }
}
