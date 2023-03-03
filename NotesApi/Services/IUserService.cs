using DB;
using Microsoft.IdentityModel.Tokens;
using NotesApi.Models.Request;
using NotesApi.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesApi.Services
{
    public interface IUserService
    {
        User? Get(int userId);

        void Add(SignUpRequest signUpRequest);

        UserResponse Auth(UserRequest requestModel);

    }
}
