using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        bool Register(string email, string username, string password);
    }

    public class AuthService : IAuthService
    {

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string email, string password)
        {
            Core.Data.DataProvider db = Core.Data.DataProvider.DataProviderFactory();
            var user = db.GetUser(email, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.guid.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Entities.User eUser = new User();
            eUser.Email = user.email;
            eUser.Username = user.username;
            eUser.Password = user.password;
            eUser.GUID = user.guid.ToString();
            eUser.Token = tokenHandler.WriteToken(token);

            return eUser.WithoutPassword();
        }

        /// <summary>
        /// Register a new account in database
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>returns true if user added to database</returns>
        public bool Register(string email, string username, string password)
        {
            Core.Data.User user = new Core.Data.User();
            user.email = email;
            user.username = username;
            user.password = password;
            Core.Data.DataProvider db = Core.Data.DataProvider.DataProviderFactory();
            return db.Insert(user);
        }

    }
}