using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Application.Features.User.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CqsResult>
    {
        private readonly IUserCommandRepository _commandRepository;
        private readonly IRolesQueryRepository _rolesQueryRepository;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IUserCommandRepository commandRepository, ILogger<CreateUserHandler> logger)
        {
            _commandRepository = commandRepository;
            _logger = logger;
        }

        public async Task<CqsResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // validations
            if (String.IsNullOrEmpty(request.Email)) return CqsResult.Failure("Email is require");
            if (String.IsNullOrEmpty(request.Password)) return CqsResult.Failure("Password is require");
            if (request.RoleId == 0) return CqsResult.Failure("Role ID is required");

            // check role existence
            CqsResult<bool> isRoleExist = await _rolesQueryRepository.IsRoleExistAsync(request.RoleId);
            if (!isRoleExist.IsSuccess) return CqsResult.Failure("This role Id not exist.");





            throw new NotImplementedException();
        }
    }
}
