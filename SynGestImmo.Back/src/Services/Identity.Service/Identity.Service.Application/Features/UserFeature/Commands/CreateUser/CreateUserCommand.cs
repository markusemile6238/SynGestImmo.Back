using MediatR;
using Tools.Result;
namespace Identity.Service.Application.Features.UserFeature.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CqsResult>
    {

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
        public string? SyndicPrefix { get; set; } = "SGI";

    }
}
