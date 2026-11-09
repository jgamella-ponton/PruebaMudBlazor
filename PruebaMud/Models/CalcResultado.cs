namespace PruebaMud.Models;

// Respuesta recibida de n8n, guardada tal cual para poder consultarla después.
public class CalcResultado
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Datos de contexto del fichero que originó el cálculo
    public string? NombreArchivo { get; set; }
    public long? RequestId { get; set; }      // ConnectRequestID del JSON
    public string? PlateCode { get; set; }

    // Respuesta completa de n8n en formato JSON
    public string RespuestaJson { get; set; } = "";
}
