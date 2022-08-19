using Newtonsoft.Json;
using System.Collections.Generic;

namespace employee.service.Entities
{
    public class VersionModel
    {
        public string Version { get; set; }
        [JsonProperty("Docs")]
        public List<VersionDocument> Documents { get; set; }
    }
}
