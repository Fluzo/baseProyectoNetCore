namespace BD_Prueba.Articulos.DTO
{
    public class FiltroArticuloDTO
    {
        public string? NOMBRE { get; set; }
        public string? FABRICANTE { get; set; }
        public string CATEGORIA { get; set; }
        public Nullable<bool> ACTIVO { get; set; }

    }
}
