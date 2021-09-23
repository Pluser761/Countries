using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows.Documents;

namespace CountryParse.Models.Adapter
{
    public class DataNode
    {
        [JsonPropertyName("name")] public StructureName StructureName { get; private set; }
        [JsonPropertyName("capital")] public List<string> Capitals { get; private set; }

        [JsonIgnore] public string Name { get; private set; }
        [JsonPropertyName("cca2")] public string Code { get; set; }
        [JsonIgnore] public string Capital { get; private set; }
        [JsonPropertyName("area")] public float Area { get; set; }
        [JsonIgnore] public int Population => 0;
        [JsonPropertyName("region")] public string Region { get; set; }

        [JsonConstructor] public DataNode(
            StructureName structureName, 
            List<string> capitals, 
            string code, float area, string region)
        {
            Code = code;
            Area = area;
            Region = region;
            Capital = "Not found";

            Name = structureName.Name;
            if (capitals != null) Capital = capitals[0];
        }

        public override string ToString()
        {
            return $"Name {Name}\n" +
                   $"Code {Code}\n" +
                   $"Capital {Capital}\n" +
                   $"Area {Area}\n" +
                   $"Region {Region}";
        }
    }

    public class StructureName
    {
        [JsonPropertyName("official")]
        public string Name { get; set; }
    }
}
