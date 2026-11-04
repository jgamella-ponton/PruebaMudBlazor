using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaMud.Models;

namespace PruebaMud.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
// public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    public DbSet<Request> Requests => Set<Request>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pedido>(e =>
        {
            e.Property(p => p.Importe).HasPrecision(18, 2);

            // Restrict: no se puede borrar un cliente que tenga pedidos
            e.HasOne(p => p.Cliente)
             .WithMany(c => c.Pedidos)
             .HasForeignKey(p => p.ClienteId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}