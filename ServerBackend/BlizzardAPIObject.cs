using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace ServerBackend
{
    public class BlizzardAPIObject
    {
        
        //Class to make requests to the Blizzard API.
        
        //Method to get the API data from the end point
        //This can be generic for all API items, just build up the endpoint URI first
        public IRestResponse MakeAPIRequest(string uri)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            IRestResponse response = client.Execute(request);

            return response;
        }

        //Compare database and API data, return data only in API (new data)
        public virtual List<T> GetNewPlayableClassesFromAPI<T>(List<T> apiList, List<T> dbList) where T : class
        {
            return apiList.Except(dbList).ToList();
        }
    }
}