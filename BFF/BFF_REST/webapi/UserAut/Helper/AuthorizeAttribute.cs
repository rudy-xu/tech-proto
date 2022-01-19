/*
    This custom authorize attribute is add to controller action methods that require the user to be authenticated.
    Authorization is performed by the OnAuthorization method which checks if there is 
    an authenticated user attached to the current request (context.HttpContext.Items["User"]). 
*/

using System;
using Microsoft.AspNetCore.Mvc.Filters;   //IAuthorizationFilter
using Microsoft.AspNetCore.Mvc;    //JsonResult
using Microsoft.AspNetCore.Http;   //StatusCodes
using WebApi.Models;

//Custom attribute -> [Authorize]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Console.WriteLine("Enter into OnAuthorization");
        var user = (UserInfo)context.HttpContext.Items["UserInfo"];
        if (user == null)
        {
            //not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
