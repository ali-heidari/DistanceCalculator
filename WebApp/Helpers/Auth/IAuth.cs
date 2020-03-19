
namespace WebApp.Helpers.Auth
{
    public interface IAuth
    {
        bool Validate(string jwt);
    }
}