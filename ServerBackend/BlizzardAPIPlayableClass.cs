using RestSharp;

namespace ServerBackend
{
    public class BlizzardAPIPlayableClass : BlizzardAPIRequest
    {
        //Stores response from the API request
        private IRestResponse apiResponse {get; set;}


        //Constructor, pass in region and authentication token
        public BlizzardAPIPlayableClass (string region, string token)
        {

        }
    }
}