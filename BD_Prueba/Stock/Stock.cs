using System;
using System.Collections.Generic;

namespace BD_Prueba.Entidades
{
    public partial class Stock
    {
        public int ID_STOCK { get; set; }
        public int ID_ARTICULO { get; set; }
        public string NOMBRE { get; set; }
        public int CANTIDAD { get; set; }
        public int LOTE { get; set; }
    }
}
