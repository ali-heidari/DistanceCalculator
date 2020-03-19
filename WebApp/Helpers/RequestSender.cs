
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApp.Helpers
{
    public class RequestSender
    {
        private static HttpClient _client;

        public RequestSender()
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("http://localhost:4500/");
            }
        }

        public async Task<HttpResponseMessage> Post(string path, object content)
        {
            StringContent postContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(path, postContent);
            return response;
        }
    }
}