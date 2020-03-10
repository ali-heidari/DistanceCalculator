using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterModel: AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

    }
}