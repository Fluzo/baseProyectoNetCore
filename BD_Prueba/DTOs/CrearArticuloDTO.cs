using BD_Prueba.Entidades;

namespace BD_Prueba.DTOs
{
    public class CrearArticuloDTO
    {
        //public int IdArticulo { get; set; }
        public string Nombre { get; set; } = null!;
        public int Alto { get; set; }
        public int Ancho { get; set; }
        public int Largo { get; set; }
        public int Peso { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Fabricante { get; set; } = null!;
        public decimal Precio { get; set; }

        //public virtual List<PedidoArticuloDTO> PedidosArticulos { get; set; }
    }
}
