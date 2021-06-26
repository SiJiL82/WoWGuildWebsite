using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServerBackend
{


    [NotMapped]
    public partial class PlayableRace
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("races")]
        public List<Race> Races { get; set; }

        public void WriteToDatabase()
        {
            using(WoWGuildContext database = new WoWGuildContext())         
                {
                    foreach(var item in Races)
                    {
                        database.Add(item);
                    }
                    
                    database.SaveChanges();
                }
        }
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
