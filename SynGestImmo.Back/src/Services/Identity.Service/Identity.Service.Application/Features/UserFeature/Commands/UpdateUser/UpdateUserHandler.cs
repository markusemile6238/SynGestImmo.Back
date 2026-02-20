using Identity.Service.Application.Common;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Domain.Repositories.UserRepositories;
using Identity.Service.Domaine.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.UpdateUser
{
    public class UpdateUserHandler(
        IUserCommandRepository userCommandRepository,
        IUserQueryRepository userQueryRepository,
        IRolesQueryRepository rolesQueryRepository,
        ISqlExceptionTranslator sqlTranslator,
        ILogger<UpdateUserHandler> logger,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserCommand, CqsResult<bool>>
    {
        private readonly IUserCommandRepository _userCommandRepository = userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository = userQueryRepository;
        private readonly IRolesQueryRepository _rolesQueryRepository = rolesQueryRepository;
        private readonly ISqlExceptionTranslator _sqlTranslator = sqlTranslator;
        private readonly ILogger<UpdateUserHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
 

        public async Task<CqsResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            #region VALIDATIONS

            if (request == null)
                return CqsResult<bool>.Failure(Error.Validation("Request is require"));

            #endregion


            try
            {
               return  await _unitOfWork.ExecuteAsync<CqsResult<bool>>(async (conn, tx) =>
                {
                     var user = await _userQueryRepository.GetUserByIdAsync(request.Id, conn, tx);

                    if (user is null)                    
                        return CqsResult<bool>.Failure(Error.NotFound("Updating Failure ! User not found."));


                    var emailCheck = await CheckEmailUniqueAsync(request, user, conn, tx);

                    if (!emailCheck.IsSuccess)
                        return emailCheck;
                  
                    var roleCheck = await CheckRoleExistenceAsync(request,user, conn, tx);

                    if (!roleCheck.IsSuccess)
                        return roleCheck;

                    ApplyPatch(request, user);               
                                     

                    // on sauve l'utilisateur
                    var updated = await _userCommandRepository.UpdateUserAsync(user, conn,tx);

                    if(!updated)
                        return CqsResult<bool>.Failure(Error.Unknown("Updating failure ! No row affected."));

                    return CqsResult<bool>.Success(true);
                });
            }
            catch (SqlException ex)
            {
                var error = _sqlTranslator.Translate(ex);
                _logger.LogError(ex, "An error occurred while updating user. Error: {ErrorMessage}", error.Message);
                return CqsResult<bool>.Failure(error);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating user.");
                return CqsResult<bool>.Failure(Error.Unknown("An unexpected error occurred."));
            }
        }

        #region ApplyPatch
        private static void ApplyPatch(UpdateUserCommand request, User user)
        {
            if(!string.IsNullOrWhiteSpace(request.Username))
                user.Username = request.Username.Trim();

            if(!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email.Trim();

            if(request.MainRoleId.HasValue)
                user.MainRoleId = request.MainRoleId.Value;

            if(request.IsActive.HasValue)
                user.IsActive = request.IsActive.Value;

            user.UpdatedAt = DateTime.UtcNow;
        } 
        #endregion

        #region CheckEmailUniqAsync
        private async Task<CqsResult<bool>> CheckEmailUniqueAsync(UpdateUserCommand request, User user, IDbConnection conn, IDbTransaction tx)
        {

            if (string.IsNullOrWhiteSpace(request.Email))
                return CqsResult<bool>.Success(true);

            if (string.Equals(request.Email, user.Email, StringComparison.OrdinalIgnoreCase))
                return CqsResult<bool>.Success(true);

            var userWithSameEmail = await _userQueryRepository.GetUserByEmailAsync(request.Email, conn, tx);

            if (userWithSameEmail is not null && userWithSameEmail.Id != user.Id)
                return CqsResult<bool>.Failure(Error.Validation("Updating failure ! Another user with this email already exists."));

            return CqsResult<bool>.Success(true);
        }
        #endregion

        #region CheckRoleAsync
        private async Task<CqsResult<bool>> CheckRoleExistenceAsync(UpdateUserCommand request, User user,IDbConnection conn, IDbTransaction tx)
        {
            if (!request.MainRoleId.HasValue)
                return CqsResult<bool>.Success(true);

            if(request.MainRoleId.Value == user.MainRoleId)
                return CqsResult<bool>.Success(true);

            var role = await _rolesQueryRepository.GetRoleByIdAsync(request.MainRoleId.Value, conn, tx);

            if (role is null)
                return CqsResult<bool>.Failure(Error.Validation("Updating failure ! Role not found."));

            return CqsResult<bool>.Success(true);
        }
        #endregion

    }

}
