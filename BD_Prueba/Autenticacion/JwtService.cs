using BD_Prueba.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BD_Prueba.Autenticacion
{
    public class JwtService
    {
        private readonly IConfiguration configuration;
        public Task<RespuestaAutenticacion> ConstruirTokenLogin(UsuarioDTO credencialesLogin)
        {
            var claims = new List<Claim>()
            {
                new Claim("ID_USUARIO", credencialesLogin.ID_USUARIO.ToString())
                };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(
                    audience: null,
                    claims: claims,
                    expires: expiracion,
                    signingCredentials: creds
                     );
            return Task.FromResult(
                                        new RespuestaAutenticacion()
                                        {
                                            ID_USUARIO = credencialesLogin.ID_USUARIO,
                                            TOKEN = new JwtSecurityTokenHandler().WriteToken(securityToken),
                                            EXPIRACION = expiracion
                                        }
          );
        }



    }
}
