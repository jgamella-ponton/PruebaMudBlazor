using Microsoft.EntityFrameworkCore;
using PruebaMud.Data;
using PruebaMud.Models;

namespace PruebaMud.Services;

public class ClienteService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public ClienteService(IDbContextFactory<AppDbContext> factory) => _factory = factory;

    public async Task<List<Cliente>> ObtenerAsync()
    {
        await using var db = await _factory.CreateDbContextAsync();
        return await db.Clientes.OrderBy(c => c.Nombre).ToListAsync();
    }

    public async Task CrearAsync(Cliente cliente)
    {
        cliente.Id = 0;
        cliente.Estado = EstadoCliente.Activo;
        cliente.Pedidos = new();

        await using var db = await _factory.CreateDbContextAsync();
        db.Clientes.Add(cliente);
        await db.SaveChangesAsync();
    }

    public async Task<bool> ActualizarAsync(Cliente cliente)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var existente = await db.Clientes.FindAsync(cliente.Id);
        if (existente is null) return false;

        existente.Nombre = cliente.Nombre;
        existente.Email = cliente.Email;
        existente.Telefono = cliente.Telefono;
        existente.Estado = cliente.Estado;
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Ok, string? Error)> BorrarAsync(int id)
    {
        await using var db = await _factory.CreateDbContextAsync();

        var existente = await db.Clientes.FindAsync(id);
        if (existente is null) return (false, "El cliente ya no existe.");

        // No se permite borrar clientes con pedidos asociados
        if (await db.Pedidos.AnyAsync(p => p.ClienteId == id))
            return (false, "No se puede borrar: el cliente tiene pedidos asociados.");

        db.Clientes.Remove(existente);
        await db.SaveChangesAsync();
        return (true, null);
    }
}
