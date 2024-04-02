//using BD_Prueba.Articulos;
//using BD_Prueba.Articulos.DTO;
//using BD_Prueba.Stock.DTO;
//using BD_Prueba.Stock.Servicios;
//using Microsoft.AspNetCore.Mvc;

//namespace BD_Prueba.Stock.Controller
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsuariosController : ControllerBase
//    {
//        public readonly IStockService _service;
//        public UsuariosController(IStockService service)
//        {
//            this._service = service;
//        }

//        [HttpGet]
//        [Route("ObtenerStockArticulos")]
//        public async Task<IActionResult> ObtenerStockArticulos()
//        {
//            StockDTO filtro = new StockDTO() { };
//            var result = _service.ObtenerStockArticulos(filtro);
//            return Ok(result);
//        }

        //[HttpPost]
        //[Route("AnadirStock")]
        //public async Task<IActionResult> AnadirStock(AnadirStockDTO dto)
        //{
        //    var result = await _service.AnadirStock(dto);
        //    return Ok(result);
        //}
//    }
//}
