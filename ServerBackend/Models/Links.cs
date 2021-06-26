using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

#nullable disable

namespace ServerBackend
{
    [NotMapped]
    public partial class Links
    {
        [JsonProperty("self")]
        public Self Self { get; set; }
    }
}
