using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
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

        //Write API data to database
        private void WriteToDatabase(List<Race> blizzardAPIObject)
        {
            using(WoWGuildContext database = new WoWGuildContext())
            {
                //Loop through each object in the list and write it to the database.
                foreach(Race item in blizzardAPIObject)
                {
                    database.Add(item);
                }
                //Commit data
                database.SaveChanges();
            }
        }

        //Pull latest data from the database
        private List<Race> GetDataFromDatabase()
        {
            List<Race> results = null;

            using(WoWGuildContext database = new WoWGuildContext())
            {
                results = database.Set<Race>().ToList();
            }

            return results;
        }

        //Compare database and API data, return data only in API (new data)
        public void WriteNewAPIDataToDatabase()
        {
            List<Race> databaseData = GetDataFromDatabase();
            //Compare existing database data against API data
            List<Race> newAPIData = GetAPIDataNotInDatabase(databaseData);
            //Write any new API data to the database
            if(newAPIData.Count > 0)
            {
                WriteToDatabase(newAPIData);
            }
        }

        //Return a list of rows that are in API data but not in the database
        private List<Race> GetAPIDataNotInDatabase(List<Race> databaseData)
        {
            return Races.Except(databaseData).ToList();
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
