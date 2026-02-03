using Identity.Service.Application.Common;
using Identity.Service.Application.DTOS.RoleDto;
using Identity.Service.Domain.Repositories.RoleRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.RoleFeature.Queries.GetAllRoles
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, CqsResult<List<ResponseRoleDto>>>
    {

        private readonly IRolesQueryRepository _rolesQueryRepository;
        private readonly ISqlExceptionTranslator _sqlHandler;
        private readonly ILogger<GetAllRolesHandler> _logger;

        public GetAllRolesHandler(IRolesQueryRepository rolesQueryRepository, ISqlExceptionTranslator sqlHandler, ILogger<GetAllRolesHandler> logger)
        {
            _rolesQueryRepository = rolesQueryRepository;
            _sqlHandler = sqlHandler;
            _logger = logger;
        }

        public async Task<CqsResult<List<ResponseRoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _rolesQueryRepository.GetAllRoleAsync();

            if (roles is null)
            {
                _logger.LogWarning("No roles found in the system.");
                return CqsResult<List<ResponseRoleDto>>.Failure(Error.Database("No roles found in the system."));
            }
            
            var response = roles.Select(role => new ResponseRoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsSystemRole = role.IsSystemRole,
                Prefixe = role.Prefixe
            }).ToList();            

            return CqsResult<List<ResponseRoleDto>>.Success(response);
        }
    }
}
