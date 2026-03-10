using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.UserDtos;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Domain.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetAllUser
{
    public class GetAllUserhandler : IRequestHandler<GetAllUserQuery,CqsResult<IEnumerable<UserDto>>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IRolesQueryRepository _rolesQueryRepository;
        private readonly ILogger<GetAllUserhandler> _logger;
        private readonly ISqlExceptionTranslator _sqlTranslator;

        public GetAllUserhandler(
            IUserQueryRepository userQueryRepository,
            ILogger<GetAllUserhandler> logger,
            ISqlExceptionTranslator sqlTranslator,
            IRolesQueryRepository rolesQueryRepository
            )
        {
            _userQueryRepository = userQueryRepository;
            _logger = logger;
            _sqlTranslator= sqlTranslator;
            _rolesQueryRepository = rolesQueryRepository;
        }

        public async Task<CqsResult<IEnumerable<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var users = await _userQueryRepository.GetAllUserAsync();

                if (users is null || !users.Any())
                {
                    _logger.LogWarning("No users found in the database.");
                    return CqsResult<IEnumerable<UserDto>>.Failure(Error.Database("No content in users database"));
                }

                var roles = await _rolesQueryRepository.GetAllRoleAsync();

                if (roles is null || !roles.Any())
                {
                    _logger.LogWarning("No roles found in the database.");
                    return CqsResult<IEnumerable<UserDto>>.Failure(Error.Database("No content foudn in role database"));
                }

                List<UserDto> userDtos = [];

                foreach (var user in users)
                {
                    UserDto userDto = new()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        UserRef = user.UserRef,
                        Email = user.Email,
                        IsEmailConfirmed = user.IsEmailConfirmed(),
                        EntityId = user.EntityId,
                        RoleId = user.MainRoleId,
                        RoleName = roles.FirstOrDefault(r => r.Id == user.MainRoleId)?.Prefixe ?? "Unknown",
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt
                    };
                    userDtos.Add(userDto);
                }

                return CqsResult<IEnumerable<UserDto>>.Success(userDtos);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Exception occurred while retrieving users.");
                return CqsResult<IEnumerable<UserDto>>.Failure(_sqlTranslator.Translate(ex));
            }
        }
    }
}
