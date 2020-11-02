using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiResource.Models;
using ApiResource.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiResource.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate requestDelegate, IOptions<AppSettings> appSettings)
        {
            _requestDelegate = requestDelegate;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = GetTokenFromHeaders(context);

            try
            {
                if (token != null)
                {
                    var jwtToken = GetValidatedToken(token);
                    await AttachUserToContext(context, userService, jwtToken);
                }
            }
            finally
            {
                await _requestDelegate(context);
            }
        }

        private string GetTokenFromHeaders(HttpContext context)
        {
            return context.Request.Headers["Authorization"]
                .FirstOrDefault()?.Split(" ").Last();
        }

        private JwtSecurityToken GetValidatedToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken) validatedToken;
        }

        private async Task AttachUserToContext(HttpContext context, IUserService userService,
            JwtSecurityToken jwtSecurityToken)
        {
            var id = Guid.Parse(jwtSecurityToken.Claims.First(x => x.Type == "id").Value);
            context.Items["User"] = await userService.GetById(id);
        }
    }
}