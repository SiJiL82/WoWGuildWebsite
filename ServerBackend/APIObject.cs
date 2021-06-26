using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace ServerBackend
{
    public class APIObject<T> : APIRequest where T : class 
    {
        

        //Write API data to database
        private void WriteToDatabase(List<T> blizzardAPIObject)
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
        private List<T> GetDataFromDatabase()
        {
            List<T> results = null;

            using(WoWGuildContext database = new WoWGuildContext())
            {
                results = database.Set<T>().ToList();
            }

            return results;
        }

        //Compare database and API data, return data only in API (new data)
        public void WriteNewAPIDataToDatabase(List<T> apiData)
        {
            List<T> databaseData = GetDataFromDatabase();
            //Compare existing database data against API data
            List<T> newAPIData = GetAPIDataNotInDatabase(apiData, databaseData);
            //Write any new API data to the database
            if(newAPIData.Count > 0)
            {
                WriteToDatabase(newAPIData);
            }
        }

        //Return a list of rows that are in API data but not in the database
        private List<T> GetAPIDataNotInDatabase(List<T> apiData, List<T> databaseData)
        {
            return apiData.Except(databaseData).ToList();
        }
        

    }
}