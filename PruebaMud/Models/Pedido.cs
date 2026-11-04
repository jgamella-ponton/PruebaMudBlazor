namespace PruebaMud.Models;

public class Pedido
{
    public int Id { get; set; }
    public decimal Importe { get; set; }
    public DateTime FechaCreacion { get; set; }
    public EstadoPedido Estado { get; set; } = EstadoPedido.Nuevo;

    // Relación con el cliente: ClienteId es la columna; Cliente es el objeto relacionado
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
}