using Identity.Service.Application.Common;
using Identity.Service.Domaine.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordHandler(IUserCommandRepository userCommandRepository, IPasswordHasher passwordHasher, ILogger<ChangePasswordHandler> logger, IUserQueryRepository userQueryRepository, ISqlExceptionTranslator sqlExceptionTranslator, IUnitOfWork unitOfWork)
        {
            _userCommandRepository = userCommandRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _userQueryRepository = userQueryRepository;
            _sqlHandler = sqlExceptionTranslator;
            _unitOfWork = unitOfWork;
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

            bool existUser = true;
            bool passwordVerified = true;

            try
            {
                await _unitOfWork.ExecuteAsync(async (conn, tx) =>
                {
                    var user = await _userQueryRepository.GetUserByEmailAsync(request.Email, conn, tx);

                    if (user == null)
                    {
                        existUser = false;
                        return;
                    }

                    if(!_passwordHasher.VerifyPassword(request.OldPassword,user.PasswordHash))
                    {
                        passwordVerified = false;
                        return;
                    }

                     var newPasswordHash = _passwordHasher.HashPassword(request.NewPassword);
                        
                     await _userCommandRepository.ChangePassword(
                         user.PasswordHash,
                         newPasswordHash,
                         request.Email,
                         conn,
                         tx);

                });
                if (!existUser)
                    return CqsResult<bool>.Failure(Error.NotFound("User not found"));
                   
                if(!passwordVerified)
                    return CqsResult<bool>.Failure(Error.Validation("Current password not match"));

                return CqsResult<bool>.Success(true);

            }
            catch (SqlException ex)
            {
                var error = _sqlHandler.Translate(ex);
                return CqsResult<bool>.Failure(error);
            }

        }
    }
}
