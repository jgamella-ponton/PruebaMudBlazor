namespace PruebaMud.Models;

public enum EstadoCliente
{
    Activo = 1,
    Inactivo = 0
}

public static class EstadoClienteExtensions
{
    // Texto para mostrar al usuario (el nombre del enum no admite espacios ni tildes)
    public static string Texto(this EstadoCliente estado) => estado switch
    {
        EstadoCliente.Activo   => "Activo",
        EstadoCliente.Inactivo => "Inactivo",
        _                      => estado.ToString()
    };
}