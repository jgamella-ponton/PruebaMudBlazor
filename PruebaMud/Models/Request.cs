//crea una clase publica llamada Request
//relación con clinentes
//los campos son ID peticion, ID cliente, fecha de solicitud, estado de la solicitud, descripción de la solicitud
// MPA, MTP, EVE, TAC seran booleanos que indican si se requiere cada dosis 
// Dosis MPA, Dosis MTP, Dosis EVE, Dosis TAC
namespace PruebaMud.Models;
public class Request        
{
    public int Id { get; set; }
    public int ClienteId { get; set; }

       // Relación con Cliente
    public Cliente? Cliente { get; set; } = new();   
  
    public DateTime FechaSolicitud { get; set; } = DateTime.Now;    
    public EstadoRequest Estado { get; set; } = EstadoRequest.Nueva;

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public string Descripcion { get; set; } = "";

    // Campos adicionales
    public int DosisMPA { get; set; }
    public int DosisMTP { get; set; }
    public int DosisEVE { get; set; }
    public int DosisTAC { get; set; } 

    // MPA, MTP, EVE, TAC seran booleanos que indican si se requiere cada dosis 
    public bool MPA { get; set; }
    public bool MTP { get; set; }   
    public bool EVE { get; set; }
    public bool TAC { get; set; }

}   