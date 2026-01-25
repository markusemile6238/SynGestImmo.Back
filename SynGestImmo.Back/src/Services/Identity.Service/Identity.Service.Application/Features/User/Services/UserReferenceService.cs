using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using Microsoft.Extensions.Logging;

namespace Identity.Service.Application.Features.User.Services
{
    public class UserReferenceService
    {
        private readonly IUserQueryRepository _userRepository;
        private readonly IRolesQueryRepository _roleRepository;
        private readonly ILogger<UserReferenceService> _logger;

        public UserReferenceService(IUserQueryRepository userRepository, IRolesQueryRepository roleRepository, ILogger<UserReferenceService> logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<string> GenerateUserRefAsync(int roleId, CancellationToken = default)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
        }

    }
}
