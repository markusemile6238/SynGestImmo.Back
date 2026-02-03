using Microsoft.AspNetCore.Authorization;

namespace Identity.Service.API.Handler
{
    public class PasswordChangeHandler : AuthorizationHandler<PasswordChangeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PasswordChangeRequirement requirement)
        {

            var identity = context.User.Identity;

            Console.WriteLine($"IsAuthenticated = {identity?.IsAuthenticated}");
            Console.WriteLine($"Claims count = {context.User.Claims.Count()}");

            var claim = context.User.FindFirst("mustChangePassword");

            // no claims => ok
            if(claim == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!bool.TryParse(claim.Value, out var mustChangePassword))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (mustChangePassword)
            {
                context.Fail();
                return Task.CompletedTask;
            }

           
            context.Succeed(requirement);
            return Task.CompletedTask;
           
        }
    }
}
