using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.UserDtos;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetAllUser
{
    public class GetAllUserhandler : IRequestHandler<GetAllUserQuery,CqsResult<IEnumerable<UserDto>>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IRolesQueryRepository _rolesQueryRepository;
        private readonly ILogger<GetAllUserhandler> _logger;
        private readonly ISqlExceptionTranslator _sqlHandler;

        public GetAllUserhandler(
            IUserQueryRepository userQueryRepository,
            ILogger<GetAllUserhandler> logger,
            ISqlExceptionTranslator sqlHandler,
            IRolesQueryRepository rolesQueryRepository
            )
        {
            _userQueryRepository = userQueryRepository;
            _logger = logger;
            _sqlHandler = sqlHandler;
            _rolesQueryRepository = rolesQueryRepository;
        }

        public async Task<CqsResult<IEnumerable<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {

            var users = await _userQueryRepository.GetAllUserAsync();
            
            if(users is null || !users.Any())
            {
                _logger.LogWarning("No users found in the database.");
                return CqsResult<IEnumerable<UserDto>>.Failure(Error.Database("No content in users database"));
            }

            var roles = await _rolesQueryRepository.GetAllRoleAsync();
            
            if(roles is null || !roles.Any())
            {
                _logger.LogWarning("No roles found in the database.");
                return CqsResult<IEnumerable<UserDto>>.Failure(Error.Database("No content foudn in role database"));
            }

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                UserDto userDto = new UserDto
                {
                    Id = user.Id,
                    UserRef = user.UserRef,
                    Email = user.Email,
                    IsEmailConfirmed = user.IsEmailConfirmed(),
                    EntityId = user.EntityId,
                    RoleId = user.MainRoleId,
                    RoleName = roles.FirstOrDefault(r => r.Id == user.MainRoleId)?.Name ?? "Unknown",
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };
                userDtos.Add(userDto);
            }

            return CqsResult<IEnumerable<UserDto>>.Success(userDtos);


        }
    }
}
