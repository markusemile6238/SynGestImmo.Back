using Identity.Service.Application.Common;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Application.Features.Auth
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, CqsResult<bool>>
    {

        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISqlExceptionTranslator _sqlHandler;
        private readonly ILogger<ChangePasswordHandler> _logger;

        public ChangePasswordHandler(IUserCommandRepository userCommandRepository, IPasswordHasher passwordHasher, ILogger<ChangePasswordHandler> logger,IUserQueryRepository userQueryRepository,ISqlExceptionTranslator sqlExceptionTranslator)
        {
            _userCommandRepository = userCommandRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _userQueryRepository = userQueryRepository;
            _sqlHandler = sqlExceptionTranslator;
        }   

        public async Task<CqsResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {


            #region VERIFICATIONS
            if (request == null)
            {
                _logger.LogWarning("Change password failed: request is null");
                return CqsResult<bool>.Failure(Error.Validation("Invalid request"));
            }

            if (string.IsNullOrWhiteSpace(request.OldPassword) || string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
            {
                _logger.LogWarning("Change password failed: one or more password fields are empty for user {Email}", request.Email);
                return CqsResult<bool>.Failure(Error.Validation("Passwords fields cannot be empty"));
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                _logger.LogWarning("Change password failed: new password and confirm password do not match for user {Email}", request.Email);
                return CqsResult<bool>.Failure(Error.Validation("New password and confirm password do not match"));
            } 
            #endregion

            var user = await _userQueryRepository.GetUserByEmailAsync(request.Email);

            bool verifPassword = _passwordHasher.VerifyPassword(request.OldPassword, user.PasswordHash);
            if(!verifPassword)
            {
                _logger.LogWarning("Change password failed: old password does not match for user {Email}", request.Email);
                return CqsResult<bool>.Failure(Error.Validation("Password no match !"));
            }
            


            var newPasswordHash = _passwordHasher.HashPassword(request.NewPassword);


            try
            {

            var result = await _userCommandRepository.ChangePassword(user.PasswordHash,newPasswordHash,request.Email);
            
            if (!result)
            {
                _logger.LogWarning("Cannot update password");
                return CqsResult<bool>.Failure(Error.Database("Cannot updated password"));
            }
                return CqsResult<bool>.Success("password changed successfully");
            }
            catch (SqlException ex)
            {
                _logger.LogError("SQL_ERROR : Cannot change password :{Message} Number:{Number}",ex.Message,ex.Number);
                var error = _sqlHandler.Translate(ex);
                return CqsResult<bool>.Failure(error);
            }
            catch(Exception ex)
            {
                _logger.LogError($"ERROR_UNEXPECTED : {ex}");
                return CqsResult<bool>.Failure(Error.Unknown($"{ex}"));

            }


        }
    }
}
