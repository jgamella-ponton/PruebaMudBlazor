namespace PruebaMud.Models;

public enum EstadoPedido
{
    Nuevo = 0,
    EnPreparacion = 1,
    Enviado = 2
}

public static class EstadoPedidoExtensions
{
    // Texto para mostrar al usuario (el nombre del enum no admite espacios ni tildes)
    public static string Texto(this EstadoPedido estado) => estado switch
    {
        EstadoPedido.Nuevo         => "Nuevo",
        EstadoPedido.EnPreparacion => "En preparación",
        EstadoPedido.Enviado       => "Enviado",
        _                          => estado.ToString()
    };
}