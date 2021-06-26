using RestSharp;

namespace ServerBackend
{
    public class APIRequest
    {
        public string MakeAPIRequest(string uri)
        {
            RestClient client = new RestClient(uri);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        
    }
}