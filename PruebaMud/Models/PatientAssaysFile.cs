namespace PruebaMud.Models;

// Estructura del fichero patient_assays.json.
// Los nombres de las propiedades coinciden con las claves del JSON.
// Todo es opcional (nullable) para que un fichero con campos que falten no rompa la lectura.

public class PatientAssaysFile
{
    public RequesterData? RequesterData { get; set; }
    public PatientData? PatientData { get; set; }
    public RequestData? RequestData { get; set; }
    public AssayData? AssayData { get; set; }
    public List<CurveData> CurvesData { get; set; } = new();
}

public class RequesterData
{
    public string? CenterID { get; set; }
    public string? CenterName { get; set; }
    public string? RequesterName { get; set; }
    public string? RequesterLastName { get; set; }
}

public class PatientData
{
    public string? InternalPatientID { get; set; }
    public string? ExternalPatientID { get; set; }
}

public class RequestData
{
    public long? ConnectRequestID { get; set; }
    public string? LIMSCode { get; set; }
    public double? Ratio { get; set; }
    public int? RequestTestObjective { get; set; }
    public DateTime? RqDateExtraction { get; set; }
    public DateTime? RqCreatedOn { get; set; }
    public string? RequesterDoctorComments { get; set; }
}

public class AssayData
{
    public long? ConnectPlateID { get; set; }
    public int? AssayId { get; set; }
    public string? PlateCode { get; set; }
    public DateTime? AssaysCreatedOn { get; set; }
    public string? MPA_Admin { get; set; }
    public string? EVE_Admin { get; set; }
    public string? TAC_Admin { get; set; }
    public string? MTP_Admin { get; set; }
}

public class CurveData
{
    public string? ImsName { get; set; }
    public double? AUC { get; set; }
    public double? IC25 { get; set; }
    public double? IC50 { get; set; }
    public double? IC75 { get; set; }
    public double? Inhib25 { get; set; }
    public double? Inhib50 { get; set; }
    public double? Inhib75 { get; set; }
    public double? Slope { get; set; }
    public double? AsinInf { get; set; }
    public double? AsinSup { get; set; }
}
