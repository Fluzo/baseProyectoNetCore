using BD_Prueba.Articulos;
using BD_Prueba.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BD_Prueba.BaseDeDatos
{
    //public class AlmacenContext: DbContext
    public class AlmacenContext : DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<V_STOCK_ARTICULOS> V_STOCK_ARTICULOS { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public AlmacenContext(DbContextOptions<AlmacenContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Articulo>(entity =>
            {
                //entity.ToTable("ARTICULOS");
                entity.ToTable("V_ARTICULOS");

                entity.Property(e => e.ID_ARTICULO).ValueGeneratedNever();

                // Definición de índice
                entity.HasKey(e => e.ID_ARTICULO);

                //// Definición de la clave foránea
                //entity.HasOne(d => d.Region)
                //    .WithMany(p => p.Territories)
                //    .HasForeignKey(d => d.RegionId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Territories_Region");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.ID_PEDIDO);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.ToTable("PERFILES");
                entity.HasKey(e => e.ID_PERFIL);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("CATEGORIAS");
                entity.HasKey(e => e.ID_CATEGORIA);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("ESTADOS");
                entity.HasKey(e => e.ID_ESTADO);
            });


            modelBuilder.Entity<V_STOCK_ARTICULOS>(entity =>
            {
               
                // Definición de índice
                entity.HasKey(e => e.ID_STOCK);

            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToView("V_USUARIOS");
                // Definición de índice
                entity.HasKey(e => e.ID_USUARIO);

            });

            modelBuilder.Entity<PedidosArticulo>(entity =>
            {
                entity.HasKey(e => e.IdPedido);
            });
        }


    }
}
