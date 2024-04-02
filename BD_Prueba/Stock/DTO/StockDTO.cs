namespace BD_Prueba.Stock.DTO
{
    public class StockDTO
    {
        public int    ID_STOCK    { get; set; }
        public int    ID_ARTICULO { get; set; }
        public string LOTE        { get; set; }
        public string NOMBRE_LOTE { get; set; }
        public int    CANTIDAD    { get; set; }
    }
}
