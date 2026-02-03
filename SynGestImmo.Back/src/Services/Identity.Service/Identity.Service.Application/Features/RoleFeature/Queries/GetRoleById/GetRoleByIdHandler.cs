using Identity.Service.Application.DTOS.RoleDto;
using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Repositories.RoleRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;
using RError = Tools.Result.Error;

namespace Identity.Service.Application.Features.RoleFeature.Queries.GetRoleById
{
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, CqsResult<ResponseRoleDto>>
    {

        private IRolesCommandRepository _commandsRepository;
        private IRolesQueryRepository _queryRepository;
        private ILogger<GetRoleByIdHandler> _logger;

        public GetRoleByIdHandler(IRolesCommandRepository commandsRepository, IRolesQueryRepository queryRepository, ILogger<GetRoleByIdHandler> logger)
        {
            _commandsRepository = commandsRepository;
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<CqsResult<ResponseRoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {

            // validations

            if (request.Id <= 0)
                return CqsResult<ResponseRoleDto>.Failure(
                    RError.Validation("Id must be greater than 0"));
        

            Role ? role;

            try
            {
                 role = await _queryRepository.GetRoleByIdAsync(request.Id);

            }catch(SqlException ex)
            {
                _logger.LogError(ex, "Database error while getting role");
                return CqsResult<ResponseRoleDto>.Failure(RError.Database("Unable to retrieve role"));
            }

            if(role == null)
            {
                return CqsResult<ResponseRoleDto>.Failure(RError.NotFound($"Role with id {request.Id} not found"));
            }



            var dto = new ResponseRoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    IsSystemRole = role.IsSystemRole,
                    Prefixe = role.Prefixe
                };

             return CqsResult<ResponseRoleDto>.Success(dto);
            

        }


    }
}

