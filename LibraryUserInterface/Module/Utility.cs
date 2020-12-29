using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExamLibrary.Module
{
    public static class Utility
    {
        private static async Task<T> DeserializeAsync<T>(HttpContent response)
        { 
            var content = await response.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<T> Exec<T>(this HttpClient client, HttpRequestMessage req)
        {
            var response = await client.SendAsync(req);

            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<T>(response.Content);

                return result;
            }


            var jresult = await response.Content.ReadAsStringAsync();

            var jObject = JObject.Parse(jresult);
            if (jObject.SelectToken("$.__wrapped") != null || jObject.SelectToken("$.__abp") != null)
            {
                jresult = jObject.SelectToken("$.error").SelectToken("$.message").ToString();
            }

            throw new Exception(jresult);
        }
    }
}