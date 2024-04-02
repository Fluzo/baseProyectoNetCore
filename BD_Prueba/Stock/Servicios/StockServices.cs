//using BD_Prueba.Articulos.DTO;
//using BD_Prueba.BaseDeDatos;
//using BD_Prueba.DTOs;
//using BD_Prueba.Entidades;
//using BD_Prueba.Stock.DTO;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using System.Data;

//namespace BD_Prueba.Stock.Servicios
//{


//    public interface IStockService
//    {
//        DbResponse<List<StockDTO>> ObtenerStockArticulos(StockDTO filtro);

//    }


//    public class StockServices
//    {
//        public AlmacenContext _ctx;
//        public StockServices(AlmacenContext ctx)
//        {
//            this._ctx = ctx;

//        }

//        public DbResponse<List<StockDTO>> ObtenerStockArticulos(StockDTO filtro)
//        {
//            List<StockDTO> stocks = this._ctx.V_STOCK_ARTICULOS.Select(s => new StockDTO()
//            {
//                ID_STOCK = s.ID_STOCK,
//                ID_ARTICULO = s.ID_ARTICULO,
//                NOMBRE_LOTE = s.NOMBRE_LOTE,
//                CANTIDAD = s.CANTIDAD,
//                LOTE = s.LOTE

//            }).ToList();
//            return new DbResponse<List<StockDTO>>(stocks);
//        }

//    }
//}
