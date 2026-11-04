namespace PruebaMud.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string? Email { get; set; }
    public string? Telefono { get; set; }

    public EstadoCliente Estado { get; set; } = EstadoCliente.Activo;
    // Un cliente puede tener muchos pedidos
    public List<Pedido> Pedidos { get; set; } = new();
}
