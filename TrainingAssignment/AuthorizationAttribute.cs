using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrainingAssignment.Helpers;

namespace TrainingAssignment
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["JWTToken"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                if (context.HttpContext.Request.Path != "/")
                {
                    context.Result = new RedirectResult("/Home/UnAuthorize");
                }
                else
                {
                    context.Result = new RedirectResult("/Home/Login");
                }
            }
            else //token found
            {
                var tokenValidate = JwtHelper.ValidateJwtToken(token); //for token validation
                if (tokenValidate == null)//token not valid
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new RedirectResult("/Home/UnAuthorize");
                }

            }
        }
    }
}
