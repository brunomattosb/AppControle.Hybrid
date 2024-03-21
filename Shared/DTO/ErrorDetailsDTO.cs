using System.Text.Json;

namespace Shared.DTO;
public class ErrorDetailsDTO
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Trace { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}