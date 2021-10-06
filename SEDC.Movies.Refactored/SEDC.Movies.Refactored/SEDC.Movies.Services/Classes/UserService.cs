using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.Movies.DataAccess.Repositories.Interfaces;
using SEDC.Movies.DomainModels;
using SEDC.Movies.RequestModels;
using SEDC.Movies.Services.Configuration;
using SEDC.Movies.Services.Interfaces;
using SEDC.Movies.DataAccess.Repositories.Interfaces;
using SEDC.Movies.DomainModels;
using SEDC.Movies.RequestModels;
using SEDC.Movies.Services.Configuration;
using SEDC.Movies.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SEDC.Movies.Services.Classes
{
    //System.IdentityModel.Tokens.Jwt
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepositoty;
        private readonly IOptions<DatabaseOptions> _options;

        public UserService(IRepository<User> userRepositoty,
                           IOptions<DatabaseOptions> options)
        {
            _userRepositoty = userRepositoty;
            _options = options;
        }

        public UserModel Authenticate(LoginRequestModel requestModel)
        {
            var hashedPassword = HashString(requestModel.Password);

            var user = _userRepositoty.GetAll()
                                      .SingleOrDefault(x =>
                                        x.Username == requestModel.Username &&
                                        x.Password == hashedPassword);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userModel = new UserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
            };

            return userModel;
        }


        public void Register(RegisterRequestModel requestModel)
        {
            if (requestModel.Password != requestModel.ConfirmPassword)
            {
                //throw new Exception("Password did not match!");
            }

            var hashedPassword = HashString(requestModel.Password);

            var user = new User()
            {
                Username = requestModel.Username,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Password = hashedPassword
            };

            _userRepositoty.Add(user);
        }

        private string HashString(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            return Encoding.ASCII.GetString(md5Data);
        }
    }
}