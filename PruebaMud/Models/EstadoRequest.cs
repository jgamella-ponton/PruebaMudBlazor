namespace PruebaMud.Models;
public enum EstadoRequest
{
    Nueva,
    EnProceso,
    Completada,
    Cancelada
} 

// crea la clase stática EstadoRequestExtensions
public static class EstadoRequestExtensions
{
    // Texto para mostrar al usuario (el nombre del enum no admite espacios ni tildes)
    public static string Texto(this EstadoRequest estado) => estado switch
    {
        EstadoRequest.Nueva      => "Nueva",
        EstadoRequest.EnProceso  => "En Proceso",
        EstadoRequest.Completada => "Completada",
        EstadoRequest.Cancelada  => "Cancelada",
        _                           => estado.ToString()
    };
}
