using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ServerBackend
{


    [NotMapped]
    public partial class PlayableRace : APIObject<Race>
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("races")]
        public List<Race> Races { get; set; }
    }

    public partial class Race
    {
        [JsonProperty("key")]
        public Self Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
    }

    public partial class PlayableRace
    {
        public static PlayableRace FromJson(string json) => JsonConvert.DeserializeObject<PlayableRace>(json, ServerBackend.Converter.Settings);
    }

    public static class PlayableRaceSerialize
    {
        public static string ToJson(this PlayableRace self) => JsonConvert.SerializeObject(self, ServerBackend.Converter.Settings);
    }
}
