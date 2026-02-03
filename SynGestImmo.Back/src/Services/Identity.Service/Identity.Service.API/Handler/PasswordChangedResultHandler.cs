using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Tools.Result;

namespace Identity.Service.API.Handler
{
    public class PasswordChangedResultHandler : IAuthorizationMiddlewareResultHandler
    {

        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next, 
            HttpContext context, 
            AuthorizationPolicy policy, 
            PolicyAuthorizationResult authorizeResult)
        {
            if(authorizeResult.Forbidden && context.User.HasClaim("mustChangePassword", "true"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    CqsResult.Failure(
                        Error.Forbidden("Password change is required before accessing this resource.")
                        )
                    );
                return;
            }
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
