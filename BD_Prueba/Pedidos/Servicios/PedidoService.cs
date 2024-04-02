using BD_Prueba.BaseDeDatos;
using BD_Prueba.DTOs;
using BD_Prueba.Pedidos.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BD_Prueba.Pedidos.Servicios
{
    public interface IPedidoService
    {
        Task<DbResponse<bool>> CrearPedido(FiltroPedidosDTO pedido);
        DbResponse<List<PedidoDTO>> DamePedidos(FiltroPedidosDTO filtro);
        Task<DbResponse<bool>> EditarPedido(PedidoDTO pedido);
        Task<DbResponse<bool>> AnadirArticuloPedido(ArticuloPedidoDTO articuloPedido);
        DbResponse<List<EstadoDTO>> DameEstados();
    }

    public class PedidoService : IPedidoService
    {
        public AlmacenContext _ctx;
        public PedidoService(AlmacenContext ctx)
        {
            this._ctx = ctx;

        }

        public DbResponse<List<PedidoDTO>> DamePedidos(FiltroPedidosDTO filtro)
        {
            List<PedidoDTO> pedidos = this._ctx.Pedidos.Select(s => new PedidoDTO()
            { ID_PEDIDO = s.ID_PEDIDO, COD_PEDIDO = s.COD_PEDIDO ,DIRECCION = s.DIRECCION, PISO = s.PISO, PUERTA = s.PUERTA, CODIGO_POSTAL = s.CODIGO_POSTAL, NOMBRE_CLIENTE = s.NOMBRE_CLIENTE, TELEFONO = s.TELEFONO, MONTANTE = s.MONTANTE, ID_ESTADO = s.ID_ESTADO }).ToList();
            return new DbResponse<List<PedidoDTO>>(pedidos);
        }

        public DbResponse<List<EstadoDTO>> DameEstados()
        {
            List<EstadoDTO> estados = this._ctx.Estados.Select(s => new EstadoDTO() { ID_ESTADO = s.ID_ESTADO, NOMBRE = s.NOMBRE }).ToList();
            return new DbResponse<List<EstadoDTO>>(estados);
        }


        public async Task<DbResponse<bool>> CrearPedido(FiltroPedidosDTO pedido)
        {

            SqlParameter Direccion = new SqlParameter()
            {
                ParameterName = "DIRECCION",
                SqlDbType = SqlDbType.VarChar,
                Size = 300,
                Value = pedido.DIRECCION
            };

            SqlParameter Piso = new SqlParameter()
            {
                ParameterName = "PISO",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.PISO
            };

            SqlParameter Puerta = new SqlParameter()
            {
                ParameterName = "PUERTA",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.PUERTA
            };

            SqlParameter CodPostal = new SqlParameter()
            {
                ParameterName = "CODIGO_POSTAL",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.CODIGO_POSTAL
            };

            SqlParameter NombreCliente = new SqlParameter()
            {
                ParameterName = "NOMBRE_CLIENTE",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.NOMBRE_CLIENTE
            };

            SqlParameter Telefono = new SqlParameter()
            {
                ParameterName = "TELEFONO",
                SqlDbType = SqlDbType.VarChar,
                Value = pedido.TELEFONO
            };

            SqlParameter Montante = new SqlParameter()
            {
                ParameterName = "MONTANTE",
                SqlDbType = SqlDbType.Decimal,
                Value = pedido.MONTANTE
            };

            SqlParameter IdEstado = new SqlParameter()
            {
                ParameterName = "ID_ESTADO",
                SqlDbType = SqlDbType.Int,
                Value = pedido.ID_ESTADO
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
                Direccion,
                Piso,
                Puerta,
                CodPostal,
                NombreCliente,
                Telefono,
                Montante,
                IdEstado,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_CREAR_PEDIDO]  @DIRECCION, @PISO, @PUERTA, @CODIGO_POSTAL, @NOMBRE_CLIENTE, @TELEFONO, @MONTANTE, @ID_ESTADO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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
        public async Task<DbResponse<bool>> AnadirArticuloPedido(ArticuloPedidoDTO articuloPedido)
        {

            SqlParameter IdPedido = new SqlParameter()
            {
                ParameterName = "ID_PEDIDO",
                SqlDbType = SqlDbType.Int,
                Value = articuloPedido.ID_PEDIDO
            };

            SqlParameter IdArticulo = new SqlParameter()
            {
                ParameterName = "ID_ARTICULO",
                SqlDbType = SqlDbType.Int,
                Value = articuloPedido.ID_ARTICULO
            };

            SqlParameter CantidadArticulo = new SqlParameter()
            {
                ParameterName = "CANTIDAD_ARTICULO",
                SqlDbType = SqlDbType.Int,
                Value = articuloPedido.CANTIDAD_ARTICULO
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
                IdPedido,
                IdArticulo,
                CantidadArticulo,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_ANADIR_ARTICULO_A_PEDIDO]  @ID_PEDIDO, @ID_ARTICULO, @CANTIDAD_ARTICULO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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


        public async Task<DbResponse<bool>> EditarPedido(PedidoDTO pedido)
        {
            //var existe = await _ctx.Pedidos.FirstOrDefaultAsync(x => x.ID_PEDIDO == id);
            //pedido.ID_PEDIDO = id;

            SqlParameter IdPedido = new SqlParameter()
            {
                ParameterName = "ID_PEDIDO",
                SqlDbType = SqlDbType.Int,
                Value = pedido.ID_PEDIDO
            };

            SqlParameter Direccion = new SqlParameter()
            {
                ParameterName = "DIRECCION",
                SqlDbType = SqlDbType.VarChar,
                Size = 300,
                Value = pedido.DIRECCION
            };
            SqlParameter Piso = new SqlParameter()
            {
                ParameterName = "PISO",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.PISO
            };

            SqlParameter Puerta = new SqlParameter()
            {
                ParameterName = "PUERTA",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.PUERTA
            };

            SqlParameter CodigoPostal = new SqlParameter()
            {
                ParameterName = "CODIGO_POSTAL",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.CODIGO_POSTAL
            };

            SqlParameter NombreCliente = new SqlParameter()
            {
                ParameterName = "NOMBRE_CLIENTE",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Value = pedido.NOMBRE_CLIENTE
            };

            SqlParameter Telefono = new SqlParameter()
            {
                ParameterName = "TELEFONO",
                SqlDbType = SqlDbType.VarChar,
                Size = 20,
                Value = pedido.TELEFONO
            };

            SqlParameter Montante = new SqlParameter()
            {
                ParameterName = "MONTANTE",
                SqlDbType = SqlDbType.Decimal,
                Value = pedido.MONTANTE
            };
            SqlParameter IdEstado = new SqlParameter()
            {
                ParameterName = "ID_ESTADO",
                SqlDbType = SqlDbType.Int,
                Value = pedido.ID_ESTADO
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
                IdPedido,
                Direccion,
                Piso,
                Puerta,
                CodigoPostal,
                NombreCliente,
                Telefono,
                Montante,
                IdEstado,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_EDITAR_PEDIDOS] @ID_PEDIDO, @DIRECCION, @PISO, @PUERTA, @CODIGO_POSTAL, @NOMBRE_CLIENTE, @TELEFONO, @MONTANTE, @ID_ESTADO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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
    }
}

