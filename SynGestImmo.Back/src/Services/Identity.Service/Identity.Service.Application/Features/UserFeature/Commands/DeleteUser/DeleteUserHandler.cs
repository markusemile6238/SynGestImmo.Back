using Identity.Service.Application.Common;
using Identity.Service.Domain.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteUserHandler(
          IUserCommandRepository userCommandRepository,
          ILogger<DeleteUserHandler> logger,
          ISqlExceptionTranslator sqlTranslator
     ) : IRequestHandler<DeleteUserCommand, CqsResult>
    {

        private readonly IUserCommandRepository _userCommandRepository = userCommandRepository;
        private readonly ILogger<DeleteUserHandler> _logger = logger;
        private readonly ISqlExceptionTranslator _sqlTranslator = sqlTranslator;




        public async Task<CqsResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            #region VALIDATIONS
            if (request == null)
                return CqsResult.Failure(Error.Validation("Request is require"));

            if (request.Id == Guid.Empty)
                return CqsResult.Failure(Error.Validation("Id cannot be empty"));  
            #endregion

            try
            {
                var affectedRow = await _userCommandRepository.DeleteUserAsync(request.Id);

                if (!affectedRow)
                {
                    _logger.LogWarning("Impossible to delete userid {UserId}",request.Id);
                    return CqsResult.Failure(Error.Database("Not possible to delete user"));
                }

                return CqsResult.Success("user was deleted successfully");
            }
            catch(SqlException ex)
            {
                var error = _sqlTranslator.Translate(ex);
                return CqsResult.Failure(error);
            }

        }
    }
}
