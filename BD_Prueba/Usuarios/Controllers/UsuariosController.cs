using Microsoft.AspNetCore.Mvc;
using BD_Prueba.Services;
using BD_Prueba.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace  BD_Prueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        public readonly IUsuariosService _service;
        private readonly IConfiguration configuration;

        public UsuariosController(IConfiguration configuration, IUsuariosService service)
        {
            this._service = service;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {

            var loginR = await _service.Login(dto);
            var autorizado = new RespuestaAutenticacion();
            if (loginR.Retcode == 0)
            {
                autorizado = await _service.ConstruirToken(loginR.Data);
            }
            else
            {
                return BadRequest("Usuario desactivado/Email no registrado");
            }
            return Ok(autorizado);
        }


        [HttpPost]
        [Route("AltaUsuario")]
        public async Task<IActionResult> AltaUsuario(UsuarioDTO dto)
        {
            var result = await _service.AltaUsuario(dto);
            
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("CrearUsuario")]
        public async Task<ActionResult<RespuestaAutenticacion>> CrearUsuario(UsuarioDTO usuario)
        {
            var result = await _service.CrearUsuario(usuario);

           return Ok(result);

        }

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(UsuarioDTO dto )
        {
            var result = await _service.EditarUsuario(dto);
            return Ok(result);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("DameUsuarios")]
        public async Task<IActionResult> DameUsuarios(string? usuario, string? email, Nullable<int> idPerfil, Nullable<bool> activo)
        {
            FiltroUsuariosDTO filtro = new FiltroUsuariosDTO()
            {
                USUARIO = usuario,
                EMAIL = email,
                ID_PERFIL = idPerfil,
                ACTIVO = activo
            };
            var result = _service.DameUsuarios(filtro);
            return Ok(result);
        }

        [HttpGet]
        [Route("DamePerfiles")]
        public async Task<IActionResult> DamePerfiles()
        {

            var result = _service.DamePerfiles();
            return Ok(result);
        }

        [HttpDelete]
        [Route("BorrarUsuario/{id:int}")]
        public async Task<IActionResult> BorrarUsuario(int id)
        {
            var result = await _service.BorrarUsuario(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("ActivarUsuario/{id:int}")]
        public async Task<IActionResult> ActivarUsuario(int id)
        {
            var result = await _service.ActivarUsuario(id);
            return Ok(result);
        }

    }


}

