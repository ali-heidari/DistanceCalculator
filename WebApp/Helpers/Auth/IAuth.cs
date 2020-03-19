
using System.Threading.Tasks;

namespace WebApp.Helpers.Auth
{
    public interface IAuth
    {
        Task<bool> ValidateAsync(string jwt);
    }
}