using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Domain.Repositories.UserRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.Service.Application.Features.UserFeature.Services
{
    public class UserReferenceService : IUserReferenceService
    {
        private readonly IUserQueryRepository _userRepository;
        private readonly IRolesQueryRepository _roleRepository;
        private readonly ILogger<UserReferenceService> _logger;
        private readonly IConfiguration _config;

        public UserReferenceService(IUserQueryRepository userRepository, IRolesQueryRepository roleRepository, ILogger<UserReferenceService> logger, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _logger = logger;
            _config = configuration;
        }

        public async Task<string> GenerateAsync(int roleId, int? year=null)
        {
    

            var  roleResult = await _roleRepository.GetRoleByIdAsync(roleId);


            //  LKIO-1-SU-2026-65123

            string prefixSyndic = _config["Syndicat:Prefix"] ?? "SYN";

            string timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

            return String.Concat(prefixSyndic,"-",roleResult!.Id,"-",roleResult.Prefixe,"-",timestamp); 


        }

        
    }
}
