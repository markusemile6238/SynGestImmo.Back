using Identity.Service.Domaine.Entities;
using Identity.Service.Infrastructure.Handlers;
using Identity.Service.Infrastructure.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserCommandRepository, IUserQueryRepository
    {
        private readonly IDapperConnection _connection;
        private readonly SqlExceptionsHandler _sqlHandler;
        private readonly UserFieldsValidator _userValidator;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            IDapperConnection connectionDapper,
            ILogger<UserRepository> logger,
            SqlExceptionsHandler sqlHandler
            )
        {
            _connection = connectionDapper;
            _sqlHandler = sqlHandler;
            _logger = logger;
        }

        public Task<CqsResult> ActivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> AssignRoleIdAsync(User user)
        {
            throw new NotImplementedException();
        }
        #region CreateUserAsync

        public async Task<CqsResult> CreateUserAsync(User user)
        {
            // validation user
            var validationUser = _userValidator.IsValid(user);
            if (validationUser.IsFailure) return validationUser;

            // check if email already exist
            var emailExist = await ExistsByEmailAsync(user.Email);
            if (emailExist.IsSuccess) return Error.Conflit("Email already exist.");

            // check is userRef already exist
            var userRefExist = await ExistsByUserRefAsync(user.UserRef);
            if (userRefExist.IsSuccess) return Error.Conflit("User ref already exist.");

            // check if EntityId exist


            try
            {

                using var connection = await _connection.CreateConnectionAsync();

                const string sql = @"
            INSERT INTO Users (Email, PasswordHash, UserRef, EntityId, MainRoleId, IsActive, CreatedAt)
            VALUES (@Email, @PasswordHash, @UserRef, @EntityId, @MainRoleId, @IsActive, @CreatedAt)";


            int newId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                user.Email,
                user.PasswordHash,
                user.UserRef,
                user.EntityId,
                user.MainRoleId,
                user.IsActive,
                user.CreatedAt
            });
                return CqsResult.Success();
            }
            catch (SqlException ex)
            {
                _logger.LogError($"SQL ERROR : {ex.Message}\n SQL ERROR NUMBER:{ex.Number}");
                return _sqlHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UNEXPECTED ERROR : Unable to create a new user {ex.Message}");
                return Error.Database("Unable to create a new user.");
            }
        }

        #endregion

        public Task<CqsResult> DeactivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        #region ExistsByEmailAsync

        public async Task<CqsResult<bool>> ExistsByEmailAsync(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            try
            {
                using var connection = await _connection.CreateConnectionAsync();
                var sql = @"SELECT COUNT(1) FROM Users WHERE Email = @Email";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { email });
                return CqsResult<bool>.Success(count > 0);
            }
            catch (SqlException ex)
            {
                _logger.LogError($"Sql Error : {ex.Message}\n Sql Code Number {ex.Number}");
                return _sqlHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected Error : Unable to verify email existence : {ex.Message}");
                return Error.Database("Unable to verify email existence");
            }

        }

        #endregion

        #region ExistsByUserRefAsync

        public async Task<CqsResult<bool>> ExistsByUserRefAsync(string userRef)
        {
            if (userRef == null) throw new ArgumentNullException(nameof(userRef));
            try
            {
                using var connection = await _connection.CreateConnectionAsync();
                var sql = @"SELECT COUNT(1) FROM Users WHERE UserRef = @UserRef";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { userRef });
                return CqsResult<bool>.Success(count > 0);
            }
            catch (SqlException ex)
            {
                _logger.LogError($"Sql Error : {ex.Message}\n Sql Code Number {ex.Number}");
                return _sqlHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected Error : Unable to verify user reference existence: {ex.Message}");
                return Error.Database("Unable to verify user reference existence");
            }
        }

        #endregion

        public Task<CqsResult<User>> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByEntityIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByUserRefAsync(string userRef)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<IEnumerable<User>>> SearchUsersAsync(string searchTerm, int limit = 50)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
