using System;
using Microsoft.AspNetCore.Http;       //StatusCodes
using Microsoft.AspNetCore.Mvc;         //JsonResult
using Microsoft.AspNetCore.Mvc.Filters;   //IAuthorizationFilter
using webApi.Models;

/*
    This custom authorize attribute is add to controller action methods that require the user to be authenticated.
    Authorization is performed by the OnAuthorization method which checks if there is 
    an authenticated user attached to the current request (context.HttpContext.Items["User"]). 
*/

namespace webApi.Helper
{
    //Custom attribute -> [Authorize]
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //throw new NotImplementedException();
            var userInfo = (UserInfo)context.HttpContext.Items["UserInfo"];
            if(userInfo == null)
            {
                //Not logged in or no Authenication
                context.Result = new JsonResult(new { message = "Unauthorized" }){
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
