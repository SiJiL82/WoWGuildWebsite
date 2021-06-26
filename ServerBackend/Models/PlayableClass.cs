using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable disable

namespace ServerBackend
{
    [NotMapped]
    public partial class PlayableClass : APIRequest
    {
        [JsonProperty("_links"), NotMapped]
        //[NotMapped]
        public Links Links { get; set; }

        [JsonProperty("classes")]
        public List<Class> Classes { get; set; }

        public void WriteToDatabase()
        {
            using(WoWGuildContext database = new WoWGuildContext())         
                {
                    foreach(var item in Classes)
                    {
                        database.Add(item);
                    }
                    
                    database.SaveChanges();
                }
        }
    }

    public partial class Class
    {
        [JsonProperty("key")]
        public Self Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
    }

    [NotMapped]
    public partial class Self
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    [NotMapped]
    public partial class Links
    {
        [JsonProperty("self")]
        public Self Self { get; set; }
    }

    public partial class PlayableClass
    {
        public static PlayableClass FromJson(string json) => JsonConvert.DeserializeObject<PlayableClass>(json, ServerBackend.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PlayableClass self) => JsonConvert.SerializeObject(self, ServerBackend.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
    

    
}
