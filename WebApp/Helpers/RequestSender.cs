
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        public RequestSender(String jwt) : this()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwt);
        }

        public async Task<HttpResponseMessage> Post(string path, object content)
        {
            StringContent postContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(path, postContent);
            return response;
        }
    }
}