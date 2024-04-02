using BD_Prueba.Articulos.DTO;
using BD_Prueba.BaseDeDatos;
using BD_Prueba.DTOs;
using BD_Prueba.Entidades;
using BD_Prueba.Stock.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BD_Prueba.Articulos.Servicios
{
    public interface IArticulosService
    {
        DbResponse<List<ArticuloDTO>> DameArticulos(FiltroArticuloDTO filtro);
        DbResponse<List<CategoriasDTO>> DameCategorias();
        Task<DbResponse<bool>> BorrarArticulo(int id);
        Task<DbResponse<bool>> ActivarArticulo(int id);
        Task<DbResponse<bool>> EditarArticulo(ArticuloDTO articulo);

        List<V_STOCK_ARTICULOS> ObtenerStockArticulos(FiltroStockArticulosDTO filtros);
        Task<DbResponse<bool>> AnadirStock(AnadirStockDTO stock);

        Task<DbResponse<bool>> CrearArticulo(ArticuloDTO articulo);

    }

    /*
     
        ####################IMPORTANTE####################

        ArticulosService implementa IArticulosService
     
     */

    public class ArticulosService : IArticulosService
    {
        public AlmacenContext _ctx;
        public ArticulosService(AlmacenContext ctx)
        {
            this._ctx = ctx;

        }
        public DbResponse<List<ArticuloDTO>> DameArticulos(FiltroArticuloDTO filtro)
        {
            List<ArticuloDTO> articulos = this._ctx.Articulos.Select(s => new ArticuloDTO() 
            { ID_ARTICULO = s.ID_ARTICULO, NOMBRE = s.NOMBRE, ALTO = s.ALTO, ANCHO = s.ANCHO, LARGO = s.LARGO, PESO = s.PESO, DESCRIPCION = s.DESCRIPCION, 
                FABRICANTE = s.FABRICANTE, PRECIO = s.PRECIO, ACTIVO = s.ACTIVO , IMAGEN = s.IMAGEN, CATEGORIA = s.CATEGORIA}).ToList();
            return new DbResponse<List<ArticuloDTO>>(articulos);
        }

        public DbResponse<List<CategoriasDTO>> DameCategorias()
        {
            List<CategoriasDTO> categorias = this._ctx.Categorias.Select(s => new CategoriasDTO() { ID_CATEGORIA = s.ID_CATEGORIA, CATEGORIA = s.CATEGORIA }).ToList();
            return new DbResponse<List<CategoriasDTO>>(categorias);
        }

        public async Task<DbResponse<bool>> BorrarArticulo(int id)
        {
            SqlParameter IdArticulo = new SqlParameter()
            {
                ParameterName = "ID_ARTICULO",
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
                IdArticulo,
                outMensaje,
                outRetcode
            };

            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_BORRAR_ARTICULO] @ID_ARTICULO,  @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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
        public async Task<DbResponse<bool>> ActivarArticulo(int id)
        {

            SqlParameter IdArticulo = new SqlParameter()
            {
                ParameterName = "ID_ARTICULO",
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
                IdArticulo,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_ACTIVAR_ARTICULO] @ID_ARTICULO,  @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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



        public async Task<DbResponse<bool>> CrearArticulo(ArticuloDTO articulo)
        {
            //SqlParameter IdArticulo = new SqlParameter()
            //{
            //    ParameterName = "ID_ARTICULO",
            //    SqlDbType = SqlDbType.Int,
            //    Value = articulo.ID_ARTICULO
            //};
            SqlParameter Nombre = new SqlParameter()
            {
                ParameterName = "NOMBRE",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = articulo.NOMBRE
            };
            SqlParameter Alto = new SqlParameter()
            {
                ParameterName = "ALTO",
                SqlDbType = SqlDbType.Int,
                Value = articulo.ALTO
            };
            SqlParameter Ancho = new SqlParameter()
            {
                ParameterName = "ANCHO",
                SqlDbType = SqlDbType.Int,
                Value = articulo.ANCHO
            };
            SqlParameter Largo = new SqlParameter()
            {
                ParameterName = "LARGO",
                SqlDbType = SqlDbType.Int,
                Value = articulo.LARGO
            };
            SqlParameter Peso = new SqlParameter()
            {
                ParameterName = "PESO",
                SqlDbType = SqlDbType.Int,
                Value = articulo.PESO
            };
            SqlParameter Descripcion = new SqlParameter()
            {
                ParameterName = "DESCRIPCION",
                SqlDbType = SqlDbType.VarChar,
                Size = 32,
                Value = articulo.DESCRIPCION
            };
            SqlParameter Fabricante = new SqlParameter()
            {
                ParameterName = "FABRICANTE",
                SqlDbType = SqlDbType.VarChar,
                Size = 32,
                Value = articulo.FABRICANTE
            };
            SqlParameter Precio = new SqlParameter()
            {
                ParameterName = "PRECIO",
                SqlDbType = SqlDbType.Int,
                Value = articulo.PRECIO
            };

            SqlParameter idCategoria = new SqlParameter()
            {
                ParameterName = "ID_CATEGORIA",
                SqlDbType = SqlDbType.Int,
                Value = articulo.ID_CATEGORIA
            };

            SqlParameter Activo = new SqlParameter()
            {
                ParameterName = "ACTIVO",
                SqlDbType = SqlDbType.Bit,
                Value = articulo.ACTIVO
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
                    //IdArticulo,
                    Nombre,
                    Alto,
                    Ancho,
                    Largo,
                    Peso,
                    Descripcion,
                    Fabricante,
                    Precio,
                    idCategoria,
                    Activo,
                    outMensaje,
                    outRetcode
                };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_CREAR_ARTICULO] @NOMBRE, @ALTO, @ANCHO, @LARGO, @PESO, @DESCRIPCION, @FABRICANTE, @PRECIO, @ID_CATEGORIA, @ACTIVO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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




        public async Task<DbResponse<bool>> EditarArticulo(ArticuloDTO articulo)

            {

            //var existe = await _ctx.Articulos.FirstOrDefaultAsync(x => x.ID_ARTICULO == id);
            //articulo.ID_ARTICULO = id;

                SqlParameter IdArticulo = new SqlParameter()
                {
                    ParameterName = "ID_ARTICULO",
                    SqlDbType = SqlDbType.Int,
                    Value = articulo.ID_ARTICULO
                };
                SqlParameter Nombre = new SqlParameter()
                {
                    ParameterName = "NOMBRE",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = articulo.NOMBRE
                };
                SqlParameter Alto = new SqlParameter()
                {
                    ParameterName = "ALTO",
                    SqlDbType = SqlDbType.Int,
                    Value = articulo.ALTO
                };
                SqlParameter Ancho = new SqlParameter()
                {
                    ParameterName = "ANCHO",
                    SqlDbType = SqlDbType.Int,
                    Value = articulo.ANCHO
                };
                SqlParameter Largo = new SqlParameter()
                {
                    ParameterName = "LARGO",
                    SqlDbType = SqlDbType.Int,
                    Value = articulo.LARGO
                };
                SqlParameter Peso = new SqlParameter()
                {
                    ParameterName = "PESO",
                    SqlDbType = SqlDbType.Int,
                    Value = articulo.PESO
                };
                SqlParameter Descripcion = new SqlParameter()
                {
                    ParameterName = "DESCRIPCION",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 32,
                    Value = articulo.DESCRIPCION
                };
                SqlParameter Fabricante = new SqlParameter()
                {
                    ParameterName = "FABRICANTE",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 32,
                    Value = articulo.FABRICANTE
                };
                SqlParameter Precio = new SqlParameter()
                {
                    ParameterName = "PRECIO",
                    SqlDbType = SqlDbType.Int,
                    Value = articulo.PRECIO
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
                    IdArticulo,
                    Nombre,
                    Alto,
                    Ancho,
                    Largo,
                    Peso,
                    Descripcion,
                    Fabricante,
                    Precio,
                    outMensaje,
                    outRetcode
                };


                 await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_EDITAR_ARTICULO] @ID_ARTICULO, @NOMBRE, @ALTO, @ANCHO, @LARGO, @PESO, @DESCRIPCION, @FABRICANTE, @PRECIO, @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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

        public List<V_STOCK_ARTICULOS> ObtenerStockArticulos(FiltroStockArticulosDTO filtros)
        {
            return this._ctx.V_STOCK_ARTICULOS.Where(x => x.CANTIDAD > 0).Select(s => new V_STOCK_ARTICULOS()
            {ID_ARTICULO = s.ID_ARTICULO, CANTIDAD = s.CANTIDAD, NOMBRE = s.NOMBRE }).ToList(); ;
        }

        public async Task<DbResponse<bool>> AnadirStock(AnadirStockDTO stock)
        {

            SqlParameter IdArticulo = new SqlParameter()
            {
                ParameterName = "ID_ARTICULO",
                SqlDbType = SqlDbType.Int,
                Value = stock.ID_ARTICULO
            };

            SqlParameter NombreLote = new SqlParameter()
            {
                ParameterName = "NOMBRE_LOTE",
                SqlDbType = SqlDbType.VarChar,
                Size = 50,
                Value = stock.NOMBRE_LOTE
            };


            SqlParameter Cantidad = new SqlParameter()
            {
                ParameterName = "CANTIDAD",
                SqlDbType = SqlDbType.Int,
                Value = stock.CANTIDAD
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
                IdArticulo,
                Cantidad,
                NombreLote,
                outMensaje,
                outRetcode
            };


            await this._ctx.Database.ExecuteSqlRawAsync("EXEC [dbo].[PA_ANADIR_STOCK] @ID_ARTICULO, @CANTIDAD, @NOMBRE_LOTE,  @MENSAJE OUTPUT, @RETCODE OUTPUT", sqlParameters);

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
