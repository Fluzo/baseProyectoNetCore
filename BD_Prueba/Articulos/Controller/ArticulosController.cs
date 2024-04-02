using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BD_Prueba.Entidades;
using AutoMapper;
using BD_Prueba.DTOs;
using BD_Prueba.BaseDeDatos;
using BD_Prueba.Articulos.Servicios;
using BD_Prueba.Articulos.DTO;
using BD_Prueba.Articulos;
using BD_Prueba.Stock.DTO;

namespace BD_Prueba.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ArticulosController : ControllerBase
    {
        public readonly IArticulosService _service;
        public ArticulosController(IArticulosService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("DameArticulos")]
        public async Task<IActionResult> DameArticulos(string? nombre, string? fabricante)
        {
            FiltroArticuloDTO filtro = new FiltroArticuloDTO()
            {
                NOMBRE = nombre,
                FABRICANTE = fabricante,
            };
            var result = _service.DameArticulos(filtro);
            return Ok(result);
        }

        [HttpGet]
        [Route("DameCategorias")]
        public async Task<IActionResult> DameCategorias()
        {

            var result = _service.DameCategorias();
            return Ok(result);
        }

        [HttpPost]
        [Route("CrearArticulo")]
        public async Task<IActionResult> CrearArticulo(ArticuloDTO dto)
        {
            var result = await _service.CrearArticulo(dto);
            return Ok(result);
        }

        [HttpPut]
        [Route("EditarArticulo")]
        public async Task<IActionResult> EditarAticulo(ArticuloDTO dto)
        {
            var result = await _service.EditarArticulo(dto);
            return Ok(result);
        }


        [HttpGet]
        [Route("ObtenerStockArticulos")]
        public ActionResult<List<V_STOCK_ARTICULOS>> ObtenerStockArticulos(string? nombreArticulo)
        {
            FiltroStockArticulosDTO filtro = new FiltroStockArticulosDTO();
            filtro.NombreArticulo = nombreArticulo;
            List<V_STOCK_ARTICULOS> stockArticulos = this._service.ObtenerStockArticulos(filtro);
            return Ok(stockArticulos);
        }

        [HttpPost]
        [Route("AnadirStock")]
        public async Task<IActionResult> AnadirStock(AnadirStockDTO dto)
        {
            var result = await _service.AnadirStock(dto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("ActivarArticulo/{id:int}")]
        public async Task<IActionResult> ActivarUsuario(int id)
        {
            var result = await _service.ActivarArticulo(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("BorrarArticulo/{id:int}")]
        public async Task<IActionResult> BorrarArticulo(int id)
        {
            var result = await _service.BorrarArticulo(id);
            return Ok(result);
        }
    }
}
