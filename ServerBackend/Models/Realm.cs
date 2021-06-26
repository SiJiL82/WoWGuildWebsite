using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ServerBackend
{
    [NotMapped]
    public partial class Realm : APIObject<RealmElement>
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("realms")]
        public List<RealmElement> Realms { get; set; }
    }

    public partial class RealmElement
    {
        [JsonProperty("key")]
        public Self Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }

    public partial class Realm
    {
        public static Realm FromJson(string json) => JsonConvert.DeserializeObject<Realm>(json, ServerBackend.Converter.Settings);
    }

    public static class RealmSerialize
    {
        public static string ToJson(this Realm self) => JsonConvert.SerializeObject(self, ServerBackend.Converter.Settings);
    }
}
