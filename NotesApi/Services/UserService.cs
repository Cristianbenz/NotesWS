using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApi.Models;
using NotesApi.Models.Request;
using NotesApi.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesApi.Services
{
    public class UserService : IUserService
    {
        private NotesContext _db;
        private readonly JwtConfiguration _jwt;

        public UserService(NotesContext dbContext, IOptions<JwtConfiguration> JwtConfig)
        {
            _db = dbContext;
            _jwt = JwtConfig.Value;
        }
        public User? Get(int userId)
        {
            /* 
                Find user by id

                Parameters: userId (int)

                Returns object of type User or null
            */
            var user = _db.Users.Where(user => user.Id == userId).FirstOrDefault();
            return user;
        }

        public void Add(SignUpRequest signUpRequest)
        {
            /* 
                Add a non existent user

                Parameters: object of type SignUpRequest
            */

            var findUser = _db.Users.Where(user => user.Email == signUpRequest.Email).FirstOrDefault();

            if (findUser == null)
            {
                string hashedPass = Encrypt.GetSHA256(signUpRequest.Password);
                User newUser = new User();
                newUser.Email = signUpRequest.Email;
                newUser.Name = signUpRequest.Name;
                newUser.Password = hashedPass;
                _db.Users.Add(newUser);
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("User already exist");
            }
        }

        public UserResponse Auth(UserRequest requestModel)
        {
            /* 
                Verify if a user with provided credentials exists

                Parameters: object of type UserRequest

                Returns object of type Response with Email and token access of the user
            */

            UserResponse response = new UserResponse();
            string hashedPass = Encrypt.GetSHA256(requestModel.Password);

            var user = _db.Users.Where(user => user.Email == requestModel.Email && user.Password == hashedPass).FirstOrDefault();

            if (user != null)
            {
                response.Id = user.Id;
                response.Token = GetJwt(user);
                return response;
            }

            throw new Exception("User not found");
        }

        private string GetJwt(User userModel)
        {
            /* 
                Generate a JWT encription with the provided info

                Parameters: object of type User

                Returns a token
            */

            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
            var tokenModel = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("Id", userModel.Id.ToString())
                    }
                    ),
                Expires= DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            };
            var token = handler.CreateToken(tokenModel);
            return handler.WriteToken(token);
        }

    }
}
