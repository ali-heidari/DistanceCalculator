
using System.Net;
using System.Threading.Tasks;
using Core.Models;

namespace WebApp.Helpers.Auth
{
    class Authentication : IAuth
    {
        public async Task<bool> ValidateAsync(string jwt)
        {
            RequestSender requestSender = new RequestSender();
            JWTModel token = new JWTModel();
            token.token = jwt;

            var res = await requestSender.Post("auth/validate", token);
            if (res.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}