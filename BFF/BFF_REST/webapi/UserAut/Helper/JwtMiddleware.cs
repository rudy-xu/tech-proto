/*
    The custom JWT middleware checks if there is a token in the request Authorization header, and if so attempts to:
        1. Validate the token
        2. Extract the user id from token
        3. Attach the authenticated user to the current HttpContext.
            Items collection to make it accessible within the scope of the current request
    If there is no token in the request header or if any of the above steps fail 
    then no user is attached to the http context and the request is only be able to access public routes. 
    Authorization is performed by the custom authorize attribute which checks that a user is attached to the http context, 
    if authorization fails a 401 Unauthorized response is returned.
*/

using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;   //JwtSecurityTokenHandler
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;  //RequestDelegate
using Microsoft.Extensions.Options;   //IOptions
using WebApi.Data;
using WebApi.Models;
using Microsoft.IdentityModel.Tokens;   //TokenValidationParameters

namespace WebApi.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;  //This Delegate would call/invoke OnAuthorization in AuthorizeAttribute.
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate requestDelegate, IOptions<AppSettings> appSettings)
        {
            _requestDelegate = requestDelegate;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserInfoRepo userInfoRepo)
        {
            //get token from the headers of httpGet
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();   //???
            Console.WriteLine($"Header: {context.Request.Headers["Authorization"]}");

            if(token != null)
            {
                // Console.WriteLine("token is null");
                
                //custom method
                await attachUserToContext(context, userInfoRepo, token);
            }

            await _requestDelegate(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserInfoRepo userInfoRepo, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    //set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                //Check whether the token is valid
                var jwtToken = (JwtSecurityToken)validatedToken;
                Console.WriteLine($"jwtToken: {jwtToken}");

                var userId = jwtToken.Claims.First(p => p.Type == "id").Value;
                context.Items["UserInfo"] = await userInfoRepo.GetUserByIdAsync(userId);

            }
            catch (System.Exception)
            {
                // user is not attached to context so request won't have access to secure routes
                // do nothing if jwt validation fails
                throw;
            }
        }
    }
}
