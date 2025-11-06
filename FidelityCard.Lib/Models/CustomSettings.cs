using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FidelityCard.Lib.Models
{
    public class CustomSettings
    {
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; } = string.Empty;

        [JsonPropertyName("endpoint_sede")]
        public string EndpointSede { get; set; } = string.Empty;
    }
}
