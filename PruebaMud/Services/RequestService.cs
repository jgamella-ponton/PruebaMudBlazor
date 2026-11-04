using Microsoft.EntityFrameworkCore;
using PruebaMud.Data;
using PruebaMud.Models;
 
namespace PruebaMud.Services;
 
public class RequestService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
 
    public RequestService(IDbContextFactory<AppDbContext> factory) => _factory = factory;
 
    // estado = null significa "todos los estados"
    // public async Task<List<Request>> ObtenerAsync(EstadoRequest? estado = null)
    // {
    //     await using var db = await _factory.CreateDbContextAsync();
 
    //     IQueryable<Request> query = db.Requests.Include(r => r.Cliente);
 
    //     if (estado.HasValue)
    //         query = query.Where(r => r.Estado == estado.Value);
 
    //     return await query
    //         .OrderByDescending(r => r.FechaSolicitud)
    //         .ThenByDescending(r => r.Id)
    //         .ToListAsync();
    // }


      public async Task<List<Request>> ObtenerAsync(OrdenRequests orden, EstadoRequest? estado = null)
    {
        await using var db = await _factory.CreateDbContextAsync();

        IQueryable<Request> query = db.Requests.Include(r => r.Cliente);

        if (estado.HasValue)
            query = query.Where(r => r.Estado == estado.Value);

        query = orden switch
        {
            OrdenRequests.FechaAsc    => query.OrderBy(r => r.FechaCreacion),
            OrdenRequests.ClienteAsc  => query.OrderBy(r => r.Cliente!.Nombre),
            OrdenRequests.FechaCreacionDesc => query.OrderByDescending(r => r.FechaCreacion),
            _                        => query.OrderByDescending(r => r.FechaCreacion)
        };

        return await query.ToListAsync();
    }

 
    // Devuelve null si la solicitud no existe
    public async Task<Request?> ObtenerPorIdAsync(int id)
    {
        await using var db = await _factory.CreateDbContextAsync();
 
        return await db.Requests
            .Include(r => r.Cliente)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
 
    public async Task CrearAsync(Request request)
    {
        request.Id = 0;                          // lo genera la base de datos
        request.FechaCreacion = DateTime.Now;    // la fecha de creación la pone el servicio
        request.Estado = EstadoRequest.Nueva;    // toda solicitud empieza como Nueva
        request.Cliente = null;                  // solo se guarda ClienteId
        NormalizarDosis(request);
 
        await using var db = await _factory.CreateDbContextAsync();
        db.Requests.Add(request);
        await db.SaveChangesAsync();
    }
 
    public async Task<bool> ActualizarAsync(Request request)
    {
        await using var db = await _factory.CreateDbContextAsync();
 
        var existente = await db.Requests.FindAsync(request.Id);
        if (existente is null) return false;
 
        NormalizarDosis(request);
 
        // FechaCreacion no se toca
        existente.ClienteId = request.ClienteId;
        existente.FechaSolicitud = request.FechaSolicitud;
        existente.Descripcion = request.Descripcion;
        existente.Estado = request.Estado;
        existente.MPA = request.MPA;
        existente.MTP = request.MTP;
        existente.EVE = request.EVE;
        existente.TAC = request.TAC;
        existente.DosisMPA = request.DosisMPA;
        existente.DosisMTP = request.DosisMTP;
        existente.DosisEVE = request.DosisEVE;
        existente.DosisTAC = request.DosisTAC;
 
        await db.SaveChangesAsync();
        return true;
    }
 
    public async Task<bool> BorrarAsync(int id)
    {
        await using var db = await _factory.CreateDbContextAsync();
 
        var existente = await db.Requests.FindAsync(id);
        if (existente is null) return false;
 
        db.Requests.Remove(existente);
        await db.SaveChangesAsync();
        return true;
    }
 
    // Si un fármaco no se requiere, su dosis se guarda siempre como 0
    private static void NormalizarDosis(Request r)
    {
        if (!r.MPA) r.DosisMPA = 0;
        if (!r.MTP) r.DosisMTP = 0;
        if (!r.EVE) r.DosisEVE = 0;
        if (!r.TAC) r.DosisTAC = 0;
    }
}
 