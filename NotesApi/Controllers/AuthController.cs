using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NotesApi.Models;
using NotesApi.Models.Request;
using NotesApi.Models.Response;
using NotesApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult SignUp([FromBody]SignUpRequest signUpRequest)
        {
            /* 
                Create a new user into the db

                Parameters: object of type SignUpRequest
            */
            Response response = new Response();
            try
            {
                _userService.Add(signUpRequest);
                response.Success = 1;
                response.Message = "User signed successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                return BadRequest(response);
            }
            
            
        }

        [HttpPost]
        public IActionResult SignIn([FromBody]UserRequest requestModel)
        {
            /* 
                Authenticate user credentials

                Parameters: object of type UserRequest
            */
            Response response = new Response();
            try
            {
                var result = _userService.Auth(requestModel);
                response.Success = 1;
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                return NotFound(response);
            }
            
            
        }
    }
}
