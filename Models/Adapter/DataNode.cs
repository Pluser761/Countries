using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace CountryParseNetCore.Models.Adapter
{
    public class DataNode
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("alpha2Code")] public string Code { get; set; }
        [JsonPropertyName("capital")] public string Capital { get; set; }
        [JsonPropertyName("area")] public float Area { get; set; }
        [JsonPropertyName("population")] public int Population { get; set; }
        [JsonPropertyName("region")] public string Region { get; set; }
    }
}
