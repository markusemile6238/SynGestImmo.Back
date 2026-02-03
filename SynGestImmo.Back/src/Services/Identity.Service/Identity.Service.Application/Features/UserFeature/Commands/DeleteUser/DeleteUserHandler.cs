using Identity.Service.Application.Common;
using Identity.Service.Domaine.Entities;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, CqsResult>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly ILogger<DeleteUserHandler> _logger;
        private readonly ISqlExceptionTranslator _sqlHandler;

        public DeleteUserHandler(IUserCommandRepository userCommandRepository, ILogger<DeleteUserHandler> logger, ISqlExceptionTranslator sqlHandler)
        {
            _userCommandRepository = userCommandRepository;
            _logger = logger;
            _sqlHandler = sqlHandler;
        }

        public async Task<CqsResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //validation
            if(request == null)
                return CqsResult.Failure(Error.Validation("Request is require"));

            if(request.Id == Guid.Empty)
                return CqsResult.Failure(Error.Validation("Id cannot be empty"));           

            try
            {
                var affectedRow = await _userCommandRepository.DeleteUserAsync(request.Id);

                if (!affectedRow)
                {
                    _logger.LogWarning($"Impossible to delete userid {request.Id}");
                    return CqsResult.Failure(Error.Database("Not possible to delete user"));
                }

                return CqsResult.Success("user was deleted successfully");
            }
            catch(SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult.Failure(error);
            }

        }
    }
}
