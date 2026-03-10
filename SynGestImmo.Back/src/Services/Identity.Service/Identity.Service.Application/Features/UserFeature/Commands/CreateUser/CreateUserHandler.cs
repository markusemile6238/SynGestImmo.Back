using Identity.Service.Application.Common;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CqsResult>
    {
        private readonly IUserCreator _userCreator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly ISqlExceptionTranslator _sqlTranslator;

        public CreateUserHandler(IUserCreator userCreator, IUnitOfWork unitOfWork, ILogger<CreateUserHandler> logger, ISqlExceptionTranslator sqlTranslator)
        {
            _userCreator = userCreator;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _sqlTranslator = sqlTranslator;
        }

        public async Task<CqsResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            #region VALIDATIONS
            if (string.IsNullOrEmpty(request.Email))
                return CqsResult.Failure(Error.Validation("Email is require"));

            if (string.IsNullOrEmpty(request.Password))
                return CqsResult.Failure(Error.Validation("Password is require"));

            if (request.RoleId <= 0)
                return CqsResult.Failure(Error.Validation("Role ID is required"));
            #endregion



            try
            {
                var result = await _unitOfWork.ExecuteAsync(async (conn, tx) =>
                {
                    return await _userCreator.CreateUserAsync(request, conn, tx);
                });

                if (result.IsSuccess)
                    _logger.LogInformation("User {Email} created successfully", request.Email);
                
                return result;






            }
            catch (SqlException ex)
            {
                var error = _sqlTranslator.Translate(ex);
                return CqsResult.Failure(error);
            }
            catch (DataException ex)
            {
                _logger.LogError(ex, "Data error while creating user {Email}", request.Email);
                return CqsResult.Failure(
                    Error.Database("Unable to create user")
                );
            }

        }
    }
}
