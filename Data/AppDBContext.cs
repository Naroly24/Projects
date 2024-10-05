using Microsoft.EntityFrameworkCore;
using AppCRUD.Models;

namespace AppCRUD.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { 

        }

        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Evento>(tb => {
                tb.HasKey(col => col.IdEvento);

                tb.Property(col => col.IdEvento)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.Nombre).HasMaxLength(50);
                tb.Property(col => col.Descripcion).HasMaxLength(50);
                tb.Property(col => col.Ubicacion).HasMaxLength(50);
            });

            modelBuilder.Entity<Evento>().ToTable("Evento");

        }


    }
}
