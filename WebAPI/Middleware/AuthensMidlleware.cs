using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Middleware
{
    public class AuthensMidlleware : IMiddleware
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;

        public AuthensMidlleware(IAccountRepository accountRepository, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
            if (!string.IsNullOrEmpty(token))
            {
                var claims = await GetAuthenticatedAccount(token);
                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Bearer"));
            }
            await next(context);
        }

        public async Task<IEnumerable<Claim>> GetAuthenticatedAccount(string accessToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            await Task.CompletedTask;

            return jwtToken.Claims;
        }
    }
}
