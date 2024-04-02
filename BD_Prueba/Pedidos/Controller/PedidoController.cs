using BD_Prueba.DTOs;
using BD_Prueba.Pedidos.DTO;
using BD_Prueba.Pedidos.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace BD_Prueba.Pedidos.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        public readonly IPedidoService _service;
        public PedidoController(IPedidoService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("DamePedidos")]
        public DbResponse<List<PedidoDTO>> DamePedidos()
        {
            FiltroPedidosDTO filtro = new FiltroPedidosDTO() {};

            var result = _service.DamePedidos(filtro);
            return result;
        }

        [HttpPost]
        [Route("CrearPedido")]
        public async Task<IActionResult> CrearPedido(FiltroPedidosDTO pedido)
        {
            var result = await _service.CrearPedido(pedido);
            return Ok(result);
        }

        [HttpPost]
        [Route("AñadirArticuloPedido")]
        public async Task<IActionResult> AnadirArticuloPedido(ArticuloPedidoDTO pedido)
        {
            var result = await _service.AnadirArticuloPedido(pedido);
            return Ok(result);
        }

        [HttpPut]
        [Route("EditarPedido")]
        public async Task<IActionResult> EditarPedido(PedidoDTO pedido)
        {          
            var result = await _service.EditarPedido(pedido);
            return Ok(result);
        }

        [HttpGet]
        [Route("DameEstados")]
        public async Task<IActionResult> DameEstados()
        {

            var result = _service.DameEstados();
            return Ok(result);
        }
    }
}
