using Microsoft.EntityFrameworkCore;
using PruebaMud.Data;
using PruebaMud.Models;

namespace PruebaMud.Services;

public class CalcResultadoService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public CalcResultadoService(IDbContextFactory<AppDbContext> factory) => _factory = factory;

    // Guarda la respuesta de n8n y devuelve el Id del registro creado
    public async Task<int> CrearAsync(string respuestaJson, string? nombreArchivo, long? requestId, string? plateCode)
    {
        var resultado = new CalcResultado
        {
            FechaCreacion = DateTime.Now,
            NombreArchivo = nombreArchivo,
            RequestId = requestId,
            PlateCode = plateCode,
            RespuestaJson = respuestaJson
        };

        await using var db = await _factory.CreateDbContextAsync();
        db.CalcResultados.Add(resultado);
        await db.SaveChangesAsync();

        return resultado.Id;
    }

    // Devuelve null si no existe
    public async Task<CalcResultado?> ObtenerPorIdAsync(int id)
    {
        await using var db = await _factory.CreateDbContextAsync();
        return await db.CalcResultados.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    // El resultado guardado más reciente; null si no hay ninguno
    public async Task<CalcResultado?> ObtenerUltimoAsync()
    {
        await using var db = await _factory.CreateDbContextAsync();
        return await db.CalcResultados
            .AsNoTracking()
            .OrderByDescending(c => c.FechaCreacion)
            .ThenByDescending(c => c.Id)
            .FirstOrDefaultAsync();
    }
}
