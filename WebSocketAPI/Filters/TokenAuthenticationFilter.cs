using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebSocketAPI.Interfaces;

namespace WebSocketAPI.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ITokenManager tokenManager = (ITokenManager) context.HttpContext.RequestServices.GetService<ITokenManager>();
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                result = false;
            }

            string token = String.Empty;

            if (result)
            {
                token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if (!tokenManager.verifyToken(token))
                {
                    result = false;
                }
            }

            if (!result) {
                 context.ModelState.AddModelError("unauthorized", "unauthorized");
            }
            context.Result = new UnauthorizedObjectResult(context.ModelState);
            
        }
    }
}
