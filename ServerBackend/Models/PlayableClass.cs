using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#nullable disable

namespace ServerBackend
{
    [NotMapped]
    public partial class PlayableClass : APIRequest
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("classes")]
        public List<Class> Classes { get; set; }

        //Write API data to database
        private void WriteToDatabase(List<Class> blizzardAPIObject)
        {
            using(WoWGuildContext database = new WoWGuildContext())
            {
                //Loop through each object in the list and write it to the database.
                foreach(var item in blizzardAPIObject)
                {
                    database.Add(item);
                }
                //Commit data
                database.SaveChanges();
            }
        }

        //Pull latest data from the database
        private List<Class> GetDataFromDatabase()
        {
            List<Class> results = null;

            using(WoWGuildContext database = new WoWGuildContext())
            {
                results = database.Set<Class>().ToList();
            }

            return results;
        }

        //Compare database and API data, return data only in API (new data)
        public void WriteNewAPIDataToDatabase()
        {
            List<Class> databaseData = GetDataFromDatabase();
            //Compare existing database data against API data
            List<Class> newAPIData = GetAPIDataNotInDatabase(databaseData);
            //Write any new API data to the database
            if(newAPIData.Count > 0)
            {
                WriteToDatabase(newAPIData);
            }
        }

        //Return a list of rows that are in API data but not in the database
        private List<Class> GetAPIDataNotInDatabase(List<Class> databaseData)
        {
            return Classes.Except(databaseData).ToList();
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

    public partial class PlayableClass
    {
        public static PlayableClass FromJson(string json) => JsonConvert.DeserializeObject<PlayableClass>(json, ServerBackend.Converter.Settings);
    }

    public static class PlayableClassSerialize
    {
        public static string ToJson(this PlayableClass self) => JsonConvert.SerializeObject(self, ServerBackend.Converter.Settings);
    }
}
