using System.Net.Http.Json;
using System.Text.Json;
using PruebaMud.Models;

namespace PruebaMud.Services;

public class N8nService
{
    private readonly HttpClient _http;
    private readonly string? _url;

    public N8nService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _url = config["N8n:WebhookUrl"];

        // Clave opcional: en n8n se valida con una credencial "Header Auth" (nombre X-Api-Key)
        var apiKey = config["N8n:ApiKey"];
        if (!string.IsNullOrWhiteSpace(apiKey))
            _http.DefaultRequestHeaders.TryAddWithoutValidation("X-Api-Key", apiKey);
    }

    // Envía los valores de las curvas a n8n y devuelve el JSON de respuesta tal cual.
    public async Task<(bool Ok, string? Error, string? Json)> EnviarCurvasAsync(
        PatientAssaysFile datos, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_url))
            return (false, "Falta configurar N8n:WebhookUrl en appsettings.json.", null);

        // Solo se envía lo necesario para el cálculo. No se incluyen datos del paciente.
        var payload = new
        {
            requestId = datos.RequestData?.ConnectRequestID,
            plateCode = datos.AssayData?.PlateCode,
            ratio = datos.RequestData?.Ratio,
            curves = datos.CurvesData
        };

        try
        {
            using var respuesta = await _http.PostAsJsonAsync(_url, payload, ct);
            var cuerpo = await respuesta.Content.ReadAsStringAsync(ct);

            if (!respuesta.IsSuccessStatusCode)
            {
                var pista = respuesta.StatusCode == System.Net.HttpStatusCode.NotFound
                    ? " Comprueba la URL del webhook: la URL de pruebas (/webhook-test/) solo funciona tras pulsar «Listen for test event» en n8n, y la de producción (/webhook/) requiere el workflow activo."
                    : "";
                return (false, $"n8n respondió {(int)respuesta.StatusCode} {respuesta.ReasonPhrase}.{pista} {Recortar(cuerpo)}", null);
            }

            if (string.IsNullOrWhiteSpace(cuerpo))
                return (false, "n8n respondió sin contenido. Comprueba que el workflow termina con un nodo «Respond to Webhook».", null);

            try
            {
                using var _ = JsonDocument.Parse(cuerpo);   // solo para validar que es JSON
            }
            catch (JsonException)
            {
                return (false, $"La respuesta de n8n no es un JSON válido: {Recortar(cuerpo)}", null);
            }

            return (true, null, cuerpo);
        }
        catch (TaskCanceledException) when (!ct.IsCancellationRequested)
        {
            return (false, "n8n tardó demasiado en responder (más de 60 segundos).", null);
        }
        catch (HttpRequestException ex)
        {
            return (false, $"No se pudo conectar con n8n: {ex.Message}", null);
        }
    }

    private static string Recortar(string texto) =>
        texto.Length <= 300 ? texto : texto[..300] + "…";
}
