using Microsoft.EntityFrameworkCore;
using PruebaMud.Data;
using PruebaMud.Models;

namespace PruebaMud.Services;

public class PedidoService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public PedidoService(IDbContextFactory<AppDbContext> factory) => _factory = factory;

    // estado = null significa "todos los estados"
    public async Task<List<Pedido>> ObtenerAsync(OrdenPedidos orden, EstadoPedido? estado = null)
    {
        await using var db = await _factory.CreateDbContextAsync();

        // Include carga también el cliente de cada pedido
        IQueryable<Pedido> query = db.Pedidos.Include(p => p.Cliente);

        if (estado.HasValue)
            query = query.Where(p => p.Estado == estado.Value);

        query = orden switch
        {
            OrdenPedidos.FechaAsc    => query.OrderBy(p => p.FechaCreacion),
            OrdenPedidos.ClienteAsc  => query.OrderBy(p => p.Cliente!.Nombre),
            OrdenPedidos.ImporteDesc => query.OrderByDescending(p => p.Importe),
            _                        => query.OrderByDescending(p => p.FechaCreacion)
        };

        return await query.ToListAsync();
    }

    public async Task CrearAsync(Pedido pedido)
    {
        pedido.Id = 0;                          // lo genera la base de datos
        pedido.FechaCreacion = DateTime.Now;    // la fecha la pone el servicio
        pedido.Estado = EstadoPedido.Nuevo;     // todo pedido empieza como Nuevo
        pedido.Cliente = null;                  // solo se guarda ClienteId

        await using var db = await _factory.CreateDbContextAsync();
        db.Pedidos.Add(pedido);
        await db.SaveChangesAsync();
    }

    public async Task<bool> ActualizarAsync(Pedido pedido)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var existente = await db.Pedidos.FindAsync(pedido.Id);
        if (existente is null) return false;

        // Solo se modifican los campos editables; FechaCreacion no se toca
        existente.ClienteId = pedido.ClienteId;
        existente.Importe = pedido.Importe;
        existente.Estado = pedido.Estado;

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> BorrarAsync(int id)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var existente = await db.Pedidos.FindAsync(id);
        if (existente is null) return false;

        db.Pedidos.Remove(existente);
        await db.SaveChangesAsync();
        return true;
    }
}
