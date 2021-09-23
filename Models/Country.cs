using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryParseNetCore.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Capital CountryCapital { get; set; }
        public float Area { get; set; }
        public int Population { get; set; }
        public Region CountryRegion { get; set; }
    }
}
