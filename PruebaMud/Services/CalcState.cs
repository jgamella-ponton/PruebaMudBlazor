using PruebaMud.Models;

namespace PruebaMud.Services;

// Estado compartido entre las páginas /calc y /calcresult.
// Es un servicio "scoped": en Blazor Server vive mientras dure la sesión del usuario (circuito).
// Si el usuario recarga el navegador (F5), se pierde.
public class CalcState
{
    // Fichero cargado en /calc (así no se pierde al volver desde /calcresult)
    public string? NombreArchivo { get; set; }
    public PatientAssaysFile? Datos { get; set; }
    public string? JsonFormateado { get; set; }

    // Última respuesta recibida de n8n
    public string? RespuestaJson { get; set; }
    public DateTime? RespuestaFecha { get; set; }

    public void LimpiarTodo()
    {
        NombreArchivo = null;
        Datos = null;
        JsonFormateado = null;
        RespuestaJson = null;
        RespuestaFecha = null;
    }
}
