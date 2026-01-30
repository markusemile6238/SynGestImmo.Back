using Identity.Service.Application.Common;
using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserRoleFeature.Commands.AssignRoleToUser
{
    public class AssignRoleToUserHandler : IRequestHandler<AssignRoleToUserCommand, CqsResult>
    {
        private readonly ISqlExceptionTranslator _SqlHandler;
        private readonly IUserRolesCommandRepository _UserRolesCommandRepository;
        private readonly ILogger<AssignRoleToUserHandler> _logger;

        public AssignRoleToUserHandler(ISqlExceptionTranslator sqlHandler, IUserRolesCommandRepository userRolesCommandRepository, ILogger<AssignRoleToUserHandler> logger)
        {
            _SqlHandler = sqlHandler;
            _UserRolesCommandRepository = userRolesCommandRepository;
            _logger = logger;
        }

        public async Task<CqsResult> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            // validation
            if (request == null)
                return CqsResult.Failure(Error.Validation("Request is require"));

            if (request.Id == Guid.Empty)
                return CqsResult.Failure(Error.Validation("Id is require"));

            if (request.roleId <= 0)
                return CqsResult.Failure(Error.Validation("roleId is require"));

            bool isAssigned;

            try
            {
                isAssigned = await _UserRolesCommandRepository.AssignRoleToUserAsync(request.Id, request.roleId);
                if (!isAssigned)
                    return CqsResult.Failure(Error.Unknown("Cannot Assigne role to user"));

                return CqsResult.Success();
            }
            catch (SqlException ex)
            {
                var error = _SqlHandler.Translate(ex);
                return CqsResult.Failure(error);
            }
            catch (DataException ex)
            {
                _logger.LogError(ex, "Data error while assigned a role to a user");
                return CqsResult.Failure(
                    Error.Database("Unable to create assigned roel to an user")
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "unexpected error");
                return CqsResult.Failure(
                   Error.Unknown("Unexpected Error [UserRole]")
               );
            }

        }
    }

}
