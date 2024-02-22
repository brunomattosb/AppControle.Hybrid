using Newtonsoft.Json;

namespace SisVendas.Shared.Responses;

public class ResponseApiCities
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("iso2")]
    public string? Iso2 { get; set; }
}
