using PruebaMud.Models;

namespace PruebaMud.Services;

// Estado compartido entre /calc y /calcresult: conserva el fichero cargado
// para que no se pierda al volver desde la página de resultados.
// Es un servicio "scoped": vive mientras dure la sesión del usuario (circuito).
// La respuesta de n8n ya no se guarda aquí, sino en la tabla CalcResultados.
public class CalcState
{
    public string? NombreArchivo { get; set; }
    public PatientAssaysFile? Datos { get; set; }
    public string? JsonFormateado { get; set; }

    public void LimpiarTodo()
    {
        NombreArchivo = null;
        Datos = null;
        JsonFormateado = null;
    }
}
