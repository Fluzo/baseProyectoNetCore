namespace BD_Prueba.Articulos.DTO
{
    public class ArticuloDTO
    {
        public int ID_ARTICULO { get; set; }
        public string NOMBRE { get; set; }
        public int ALTO { get; set; }
        public int ANCHO { get; set; }
        public int LARGO { get; set; }
        public int PESO { get; set; }
        public string DESCRIPCION { get; set; }
        public string FABRICANTE { get; set; }
        public decimal PRECIO { get; set; }
        public bool ACTIVO { get; set; }
        public string IMAGEN { get; set; }
        public string CATEGORIA { get; set; }
        public int ID_CATEGORIA { get; set; }
    }
}
