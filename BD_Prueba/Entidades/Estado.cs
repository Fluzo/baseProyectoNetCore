using System;
using System.Collections.Generic;

namespace BD_Prueba.Entidades
{
    public partial class Estado
    {
        //public Estado()
        //{
        //    Pedidos = new HashSet<Pedido>();
        //}

        public int ID_ESTADO { get; set; }
        public string NOMBRE { get; set; }

        //public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
