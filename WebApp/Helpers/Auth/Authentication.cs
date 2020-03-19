
namespace WebApp.Helpers.Auth
{
    class Authentication : IAuth
    {
        public bool Validate(string jwt)
        { 
            return true;
        }
    }
}