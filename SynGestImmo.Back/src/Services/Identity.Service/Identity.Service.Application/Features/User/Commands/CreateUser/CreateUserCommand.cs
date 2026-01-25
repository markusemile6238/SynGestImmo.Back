using MediatR;
using Tools.Result;
namespace Identity.Service.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CqsResult>
    {
        public string? SyndicPrefix { get; set; } = "SGI";
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;

    }
}
