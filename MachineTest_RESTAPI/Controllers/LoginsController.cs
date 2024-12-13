using MachineTest_RESTAPI.Model;
using MachineTest_RESTAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MachineTest_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        //Get configurations from appsettings - secretKey
        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;

        //DI
        public LoginsController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }

        #region  1 - User Registration
        [HttpPost("nu")]
        public async Task<IActionResult> Register([FromBody] UserRegistration newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var registeredUser = await _loginRepository.RegisterUser(newUser);
                return CreatedAtAction(nameof(Register), new { id = registeredUser.UserId }, registeredUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region  2 - Login Validation
        [AllowAnonymous]
        [HttpGet("{username}/{userpass}")]
        public async Task<IActionResult> GetUserValidation(string username, string userpass)
        {
            IActionResult response = Unauthorized();
            UserRegistration validUser = await _loginRepository.UserValidation(username, userpass);

            if (validUser != null)
            {
                var tokenString = GenerateJWTToken(validUser);
                response = Ok(new
                {
                    Uname = validUser.Username,
                    RoleId = validUser.RoleId,
                    Token = tokenString
                });
            }

            return response;
        }
        #endregion


        #region  3 -  Get All Users
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> GetAllUserTbl()
        {
            var ulogin = await _loginRepository.GetAllUsers();
            if (ulogin == null)
            {
                return NotFound("No users found ");
            }
            return Ok(ulogin);
        }
        #endregion

        #region  4 - Search By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistration>> GetSearchById(int id)
        {
            var user = await _loginRepository.UserSearchById(id);
            if (user == null)
            {
                return NotFound("No employees found ");
            }
            return Ok(user);
        }
        #endregion

        #region  5 - Login Update
        [HttpPut("{id}")]
        public async Task<ActionResult<UserRegistration>> PutUpdatelogin(int id, UserRegistration register)
        {
            if (ModelState.IsValid)
            {
                var update = await _loginRepository.UpdateLogin(id, register);
                if (update != null)
                {
                    return Ok(update);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion

        #region  6 - Delete by id
        [HttpDelete("{id}")]
        public IActionResult Deletelogin(int id)
        {
            try
            {
                var result = _loginRepository.Deletelogin(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "user could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region 7 - Generate JWT Token
        private string GenerateJWTToken(UserRegistration validUser)
        {
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
           
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: null,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}