using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

#nullable disable

namespace ServerBackend
{
    [NotMapped]
    public partial class PlayableClass : APIObject<apiClass>
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("classes")]
        public List<apiClass> Classes { get; set; }        
    }

    public partial class apiClass
    {
        [JsonProperty("key")]
        public Self Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
    }

    public partial class PlayableClass
    {
        public static PlayableClass FromJson(string json) => JsonConvert.DeserializeObject<PlayableClass>(json, ServerBackend.Converter.Settings);
    }

    public static class PlayableClassSerialize
    {
        public static string ToJson(this PlayableClass self) => JsonConvert.SerializeObject(self, ServerBackend.Converter.Settings);
    }
}
