using LearningModels;
using LearningWebApi.DBContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using LearningWebApi.Utilities;
using Microsoft.AspNetCore.Http;

namespace LearningWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly JWTSettings jwtsettings;

        public UsersController(IUserRepository IUserRepository, IOptions<JWTSettings> JWTSettings)
        {
            this.userRepository = IUserRepository;
            this.jwtsettings = JWTSettings.Value;
        }


        // POST: api/Users
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserWithToken>> RegisterUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                //Add users
                var result = await userRepository.AddUser(user);

                //load role for registered user
                user = await userRepository.GetUserByID(user.UserId);

                UserWithToken userWithToken = null;

                if (user != null)
                {
                    //Generate token
                    UserRefreshToken refreshToken = GenerateToken.GenerateRefreshToken();

                    //Add generated token to database
                    await userRepository.AddUserRefreshToken(user, refreshToken);


                    userWithToken = new UserWithToken(user);
                    userWithToken.RefreshToken = refreshToken.Token;
                }

                if (userWithToken == null)
                {
                    return NotFound();
                }

                //sign your token here here..
                userWithToken.AccessToken = GenerateToken.GenerateAccessToken(user.UserId, jwtsettings);

                return Ok(userWithToken);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        {
            try
            {
                user = await userRepository.ValidateUserFromEmailAndPasswords(user.Email, user.Password);

                UserWithToken userWithToken = null;

                if (user != null)
                {
                    //Generate token
                    UserRefreshToken refreshToken = GenerateToken.GenerateRefreshToken();

                    //Add generated token to database
                    await userRepository.AddUserRefreshToken(user, refreshToken);


                    userWithToken = new UserWithToken(user);
                    userWithToken.RefreshToken = refreshToken.Token;

                    if (userWithToken == null)
                    {
                        return NotFound();
                    }

                    //sign your token here here..
                    userWithToken.AccessToken = GenerateToken.GenerateAccessToken(user.UserId, jwtsettings);
                    return Ok(userWithToken);
                }
                else
                {
                    return NotFound("User not found");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("GetUserByAccessToken")]
        [HttpPost]
        public async Task<ActionResult<User>> GetUserByAccessToken([FromBody] string accessToken)
        { 
            try
            { 
                string userID = GenerateToken.GetUserIdFromAccessToken(accessToken, jwtsettings);

                //return await _context.Users.Include(u => u.Role)
                //                      .Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();

                var user = await userRepository.GetUserByID(Convert.ToInt32(userID));
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
