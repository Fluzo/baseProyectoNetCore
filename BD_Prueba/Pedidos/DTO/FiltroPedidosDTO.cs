namespace BD_Prueba.Pedidos.DTO
{
    public class FiltroPedidosDTO
    {
        public string  DIRECCION { get; set; }
        public string  PISO { get; set; }
        public string  PUERTA { get; set; }
        public string  CODIGO_POSTAL { get; set; }
        public string  NOMBRE_CLIENTE { get; set; }
        public string  TELEFONO { get; set; }
        public decimal MONTANTE { get; set; }
        public int     ID_ESTADO { get; set; }
    }
}
