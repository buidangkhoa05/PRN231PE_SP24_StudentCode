using BusinessObject;
using BusinessObject.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenController : ControllerBase
    {
        private IConfiguration _config;

        private readonly IAccountRepository _accountRepository;

        public AuthenController(IConfiguration config, IAccountRepository accountRepository)
        {
            _config = config;
            _accountRepository = accountRepository;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();

            var user = await _accountRepository.GetAccountByEmail(login.EmailAddress);

            if (user == null || user.AccountPassword != login.AccountPassword)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenString = GenerateJSONWebToken(user);
            response = Ok(new { token = tokenString });

            return response;
        }

        private string GenerateJSONWebToken(BranchAccount userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = (AccountRole)(userInfo.Role ?? 0);

            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, userInfo.EmailAddress),
                new Claim(ClaimTypes.Name, userInfo.FullName),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim("AccountID", userInfo.AccountId.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(300),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

