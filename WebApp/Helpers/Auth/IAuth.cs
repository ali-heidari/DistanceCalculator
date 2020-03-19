
using System.Threading.Tasks;

namespace WebApp.Helpers.Auth
{
    public interface IAuth
    {
        Task<bool> ValidateAsync(string jwt);       
        Task<bool> Logout(string jwt);
    }
}