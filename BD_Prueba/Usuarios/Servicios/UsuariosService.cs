using BD_Prueba.BaseDeDatos;
using BD_Prueba.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BD_Prueba.Services
{

    public interface IUsuariosService
    {
        Task<DbResponse<LoginResponseDTO>> Login(LoginUsuarioDTO login);
        Task<DbResponse<bool>> AltaUsuario(UsuarioDTO usuario);
        Task<DbResponse<bool>> CrearUsuario(UsuarioDTO usuario);
        Task<DbResponse<bool>> EditarUsuario(UsuarioDTO usuario);
        DbResponse<List<UsuarioDTO>> DameUsuarios(FiltroUsuariosDTO filtro);
        Task<DbResponse<bool>> BorrarUsuario(int id);
        Task<DbResponse<bool>> ActivarUsuario(int id);
        DbResponse<List<PerfileDTO>> DamePerfiles();
        Task<RespuestaAutenticacion> ConstruirToken(LoginResponseDTO credenciales);
    }
    public class UsuariosService : IUsuariosService
    {
        public AlmacenContext _ctx;
        private readonly IConfiguration configuration;

        public UsuariosService(AlmacenContext ctx, IConfiguration configuration)
        {
            this._ctx = ctx;
            this.configuration = configuration;

        }


        public Task<RespuestaAutenticacion> ConstruirToken(LoginResponseDTO credenciales)
        {
            var claims = new List<Claim>()
            {
                new Claim("ID_USUARIO", credenciales.ID_USUARIO.ToString()),

            };
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
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
                                            ID_USUARIO = credenciales.ID_USUARIO,
                                            ID_PERFIL = credenciales.ID_PERFIL,
                                            TOKEN = new JwtSecurityTokenHandler().WriteToken(securityToken),
                                            EXPIRACION = expiracion
                                        }
          );
        }






        public async Task<DbResponse<bool>> BorrarUsuario(int id)
        {

            SqlParameter IdUsuario = new SqlParameter()
            {
                ParameterName = "ID_USUARIO",
                SqlDbType = SqlDbType.Int,
                Value = id
            };

            SqlParameter outMensaje = new SqlParameter()
            {
                ParameterName = "MENSAJE",
                SqlDbType = SqlDbType.VarChar,
                Size = 2000,
                Direction = ParameterDirection.Output
            };
            SqlParameter outRetcode = new SqlParameter()
            {
                ParameterName = "RETCODE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var sqlParameters = new[]
            {
                IdUsuario,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_BORRAR_USUARIO] @ID_USUARIO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

            if ((int)outRetcode.Value != 0)
            {
                return new DbResponse<bool>(false)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
            else
            {
                return new DbResponse<bool>(true)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }            
        }



        public async Task<DbResponse<bool>> ActivarUsuario(int id)
        {

            SqlParameter IdUsuario = new SqlParameter()
            {
                ParameterName = "ID_USUARIO",
                SqlDbType = SqlDbType.Int,
                Value = id
            };

            SqlParameter outMensaje = new SqlParameter()
            {
                ParameterName = "MENSAJE",
                SqlDbType = SqlDbType.VarChar,
                Size = 2000,
                Direction = ParameterDirection.Output
            };
            SqlParameter outRetcode = new SqlParameter()
            {
                ParameterName = "RETCODE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var sqlParameters = new[]
            {
                IdUsuario,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_ACTIVAR_USUARIO] @ID_USUARIO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

            if ((int)outRetcode.Value != 0)
            {
                return new DbResponse<bool>(false)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
            else
            {
                return new DbResponse<bool>(true)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
        }



        public async Task<DbResponse<LoginResponseDTO>> Login(LoginUsuarioDTO login)
        {
            var result = await this.PaLogin(login);
            return result;
        }

        public DbResponse<List<UsuarioDTO>> DameUsuarios(FiltroUsuariosDTO filtro)
        {
            List<UsuarioDTO> usuarios = this._ctx.Usuarios.Select(s => new UsuarioDTO()
            { ID_USUARIO = s.ID_USUARIO, USUARIO = s.USUARIO, EMAIL = s.EMAIL, PASSWORD = s.PASSWORD, ID_PERFIL = s.ID_PERFIL, PERFIL = s.PERFIL, ACTIVO = s.ACTIVO }).ToList();
            return new DbResponse<List<UsuarioDTO>>(usuarios);
        }

        public DbResponse<List<PerfileDTO>> DamePerfiles()
        {
            List<PerfileDTO> perfiles = this._ctx.Perfiles.Select(s => new PerfileDTO() { IdPerfil = s.ID_PERFIL, Perfil = s.PERFIL}).ToList();
            return new DbResponse<List<PerfileDTO>>(perfiles);
        }


        public async Task<DbResponse<bool>> AltaUsuario(UsuarioDTO usuario)
        {
            SqlParameter Usuario = new SqlParameter()
            {
                ParameterName = "USUARIO",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.USUARIO
            };
            SqlParameter Password = new SqlParameter()
            {
                ParameterName = "PASSWORD",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.PASSWORD
            };
            SqlParameter Email = new SqlParameter()
            {
                ParameterName = "EMAIL",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.EMAIL
            };
            SqlParameter IdPerfil = new SqlParameter()
            {
                ParameterName = "ID_PERFIL",
                SqlDbType = SqlDbType.Int,
                Value = usuario.ID_PERFIL
            };
            //SqlParameter Activo = new SqlParameter()
            //{
            //    ParameterName = "ACTIVO",
            //    SqlDbType = SqlDbType.Bit,
            //    Value = usuario.Activo
            //};

            SqlParameter outMensaje = new SqlParameter()
            {
                ParameterName = "MENSAJE",
                SqlDbType = SqlDbType.VarChar,
                Size = 2000,
                Direction = ParameterDirection.Output
            };
            SqlParameter outRetcode = new SqlParameter()
            {
                ParameterName = "RETCODE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var sqlParameters = new[]
            {
                Usuario,
                Password,
                Email,
                IdPerfil,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_ALTA_USUARIO] @USUARIO, @PASSWORD, @EMAIL, @ID_PERFIL, @MENSAJE OUTPUT,@RETCODE OUTPUT", sqlParameters);

            if ((int)outRetcode.Value != 0)
            {
                return new DbResponse<bool>(false)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
            else
            {
                return new DbResponse<bool>(true)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
        }

        public async Task<DbResponse<bool>> CrearUsuario(UsuarioDTO usuario)
        {

            //var result = await _service.CrearUsuario(usuario);

            //var user = new IdentityUser { UserName = usuario.USUARIO, Email = usuario.EMAIL };
            //var resultado = await userManager.CreateAsync(user, usuario.PASSWORD);


            SqlParameter Usuario = new SqlParameter()
            {
                ParameterName = "USUARIO",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.USUARIO
            };
            SqlParameter Password = new SqlParameter()
            {
                ParameterName = "PASSWORD",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.PASSWORD
            };
            SqlParameter Email = new SqlParameter()
            {
                ParameterName = "EMAIL",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.EMAIL
            };

            SqlParameter outIdPerfil = new SqlParameter()
            {
                ParameterName = "ID_PERFIL",
                SqlDbType = SqlDbType.Int,
                Value = usuario.ID_PERFIL,
                Direction = ParameterDirection.Output
            };

            SqlParameter outActivo = new SqlParameter()
            {
                ParameterName = "ACTIVO",
                SqlDbType = SqlDbType.Bit,
                Value = usuario.ACTIVO,
                Direction = ParameterDirection.Output
            };

            SqlParameter outMensaje = new SqlParameter()
            {
                ParameterName = "MENSAJE",
                SqlDbType = SqlDbType.VarChar,
                Size = 2000,
                Direction = ParameterDirection.Output
            };
            SqlParameter outRetcode = new SqlParameter()
            {
                ParameterName = "RETCODE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var sqlParameters = new[]
            {
                Usuario,
                Password,
                Email,
                outIdPerfil,
                outActivo,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_CREAR_USUARIO] @USUARIO, @PASSWORD, @EMAIL, @ID_PERFIL OUTPUT, @ACTIVO OUTPUT, @MENSAJE OUTPUT,@RETCODE OUTPUT", sqlParameters);

            if ((int)outRetcode.Value != 0)
            {
                return new DbResponse<bool>(false)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
            else
            {
                return new DbResponse<bool>(true)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
        }

        public async Task<DbResponse<bool>> EditarUsuario(UsuarioDTO usuario )
        {

            //var existe = await _ctx.Usuarios.FirstOrDefaultAsync(x => x.ID_USUARIO == id);
            //usuario.ID_USUARIO = id;

            SqlParameter Usuario = new SqlParameter()
            {
                ParameterName = "USUARIO",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.USUARIO
            };

            SqlParameter Email = new SqlParameter()
            {
                ParameterName = "EMAIL",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = usuario.EMAIL
            };

            SqlParameter Password = new SqlParameter()
            {
                ParameterName = "PASSWORD",
                SqlDbType = SqlDbType.VarChar,
                Size = 32,
                Value = usuario.PASSWORD
            };

            SqlParameter IdPerfil = new SqlParameter()
            {
                ParameterName = "ID_PERFIL",
                SqlDbType = SqlDbType.Int,
                Value = usuario.ID_PERFIL
            };

            SqlParameter IdUsuario = new SqlParameter()
            {
                ParameterName = "ID_USUARIO",
                SqlDbType = SqlDbType.Int,
                Value = usuario.ID_USUARIO
            };


            SqlParameter outMensaje = new SqlParameter()
            {
                ParameterName = "MENSAJE",
                SqlDbType = SqlDbType.VarChar,
                Size = 2000,
                Direction = ParameterDirection.Output
            };
            SqlParameter outRetcode = new SqlParameter()
            {
                ParameterName = "RETCODE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var sqlParameters = new[]
            {   
                Usuario,
                Password,
                Email,
                IdPerfil,
                IdUsuario,
                outMensaje,
                outRetcode
            };

            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_EDITAR_USUARIO] @USUARIO, @PASSWORD, @EMAIL, @ID_PERFIL, @ID_USUARIO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

            if ((int)outRetcode.Value != 0)
            {
                return new DbResponse<bool>(false)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
            else
            {
                return new DbResponse<bool>(true)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
        }

        private async Task<DbResponse<LoginResponseDTO>> PaLogin(LoginUsuarioDTO login)
        {
            SqlParameter Email = new SqlParameter()
            {
                ParameterName = "EMAIL",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = login.EMAIL
            };
            SqlParameter Password = new SqlParameter()
            {
                ParameterName = "PASSWORD",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = login.PASSWORD
            };
            SqlParameter outIdPerfil = new SqlParameter()
            {
                ParameterName = "ID_PERFIL",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            SqlParameter outIdUsuario = new SqlParameter()
            {
                ParameterName = "ID_USUARIO",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            SqlParameter outMensaje = new SqlParameter()
            {
                ParameterName = "MENSAJE",
                SqlDbType = SqlDbType.VarChar,
                Size = 2000,
                Direction = ParameterDirection.Output
            };
            SqlParameter outRetcode = new SqlParameter()
            {
                ParameterName = "RETCODE",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var sqlParameters = new[]
            {
                Email,
                Password,
                outIdPerfil,
                outIdUsuario,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_LOGIN] @EMAIL, @PASSWORD, @ID_PERFIL OUTPUT, @ID_USUARIO OUTPUT, @MENSAJE OUTPUT,@RETCODE OUTPUT", sqlParameters);

            if ((int)outRetcode.Value != 0)
            {
                return new DbResponse<LoginResponseDTO>(new LoginResponseDTO())
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value
                };
            }
            else
            {
                LoginResponseDTO usuario = new LoginResponseDTO((int)outIdUsuario.Value,(int)outIdPerfil.Value);

                return new DbResponse<LoginResponseDTO>(usuario)
                {
                    Mensaje = outMensaje.Value.ToString(),
                    Retcode = (int)outRetcode.Value,
                    Data = usuario
                };
            }


        }


    }
}
