using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

#nullable disable

namespace ServerBackend
{
    [NotMapped]
    public partial class Self
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }
}
