using ComercioApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComercioApi.Infrastructure.Data;

public class ComercioDbContext : DbContext
{
    public ComercioDbContext(DbContextOptions<ComercioDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Municipio> Municipios => Set<Municipio>();
    public DbSet<Comerciante> Comerciantes => Set<Comerciante>();
    public DbSet<Establecimiento> Establecimientos => Set<Establecimiento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nombre).HasMaxLength(100);
            e.Property(x => x.Correo).HasMaxLength(255);
            e.Property(x => x.PasswordHash).HasMaxLength(500);
            e.Property(x => x.Rol).HasMaxLength(50);
        });

        modelBuilder.Entity<Municipio>(e =>
        {
            e.ToTable("Municipios");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Comerciante>(e =>
        {
            e.ToTable("Comerciantes");
            e.HasKey(x => x.Id);
            e.ToTable(tb=>tb.HasTrigger("trg_Comerciantes_Auditoria"));
            e.Property(x => x.NombreRazonSocial).HasColumnName("NombreRazonSocial").HasMaxLength(200);
            e.Property(x => x.Telefono).HasMaxLength(20);
            e.Property(x => x.Correo).HasMaxLength(255);
            e.Property(x => x.Estado).HasMaxLength(20);
            e.Property(x => x.UsuarioModifica).HasMaxLength(100);
            e.HasOne(x => x.Municipio).WithMany(m => m.Comerciantes).HasForeignKey(x => x.MunicipioId);
        });

        modelBuilder.Entity<Establecimiento>(e =>
        {
            e.ToTable("Establecimientos");
            e.ToTable(tb=>tb.HasTrigger("trg_Establecimientos_Auditoria"));
            e.HasKey(x => x.Id);
            e.Property(x => x.NombreEstablecimiento).HasColumnName("NombreEstablecimiento").HasMaxLength(200);
            e.Property(x => x.Ingresos).HasPrecision(18, 2);
            e.Property(x => x.UsuarioModifica).HasMaxLength(100);
            e.HasOne(x => x.Comerciante).WithMany(c => c.Establecimientos).HasForeignKey(x => x.ComercianteId);
        });
    }
}
