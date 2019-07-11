using SearchRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace SearchRepo.Helpers
{
    public static class HttpHelper
    {

        public static async  Task<List<Item>> GetReposityList(string query)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "SearchRepo");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Uri uri = new Uri("https://api.github.com/search/repositories?q=" + query); // MOVE TO WEBCONFIG
                    HttpResponseMessage response = await client.GetAsync(uri);
                    string textJson = await response.Content.ReadAsStringAsync();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Repository result = serializer.Deserialize<Repository>(textJson);
                    return result.Items;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
           
        }
    }
}