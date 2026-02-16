using Identity.Service.Domain.Exceptions;
using Identity.Service.Domaine.Entities;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserCommandRepository, IUserQueryRepository
    {
        private readonly IDapperConnection _connection;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            IDapperConnection connectionDapper,
            ILogger<UserRepository> logger
            )
        {
            _connection = connectionDapper;
            _logger = logger;
        }

        public Task<bool> ActivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        // commands

        #region CreateUserAsync
        public async Task<Guid> CreateUserAsync(User user, IDbConnection connection, IDbTransaction transaction)
        {

            user.Id = Guid.NewGuid(); // creation officel de l'id
            //using var connection = await _connection.CreateConnectionAsync();

            const string sql = @"
                INSERT INTO Users (Id, Email, PasswordHash, UserRef, EntityId, MainRoleId, IsActive, CreatedAt)            
                VALUES (@Id, @Email, @PasswordHash, @UserRef, @EntityId, @MainRoleId, @IsActive, @CreatedAt)
                ;";


            int rowAffected = await connection.ExecuteAsync(sql, user, transaction);


            //await AssignRoleIdAsync(user.MainRoleId, newId);

            if (rowAffected != 1)
                throw new IdentityServiceException("Insert user Failed");

            return user.Id;

        }

        #endregion
    
        #region AssignRolesToUserAsynch

        public async Task<bool> AssignRoleIdAsync(int roleId, Guid userId)
        {

                using var connection = await _connection.CreateConnectionAsync();
                const string sql = @"
                    UPDATE Users 
                    SET MainRoleId =  @roleId
                    WHERE Id = @userId";
                
                var rowAffected = await connection.ExecuteAsync(sql, new 
                { 
                    roleId,
                    userId 
                });

            return rowAffected > 0;

        }

        #endregion

        #region ChangePassword
        public async Task<bool> ChangePassword(string oldPasswordHash, string newPasswordHash, string email, IDbConnection conn,IDbTransaction tx)
        {

            const string sql = @"
                UPDATE Users 
                SET 
                    PasswordHash = @NewPasswordHash,
                    MustChangePassword = 0
                WHERE Email = @Email AND PasswordHash = @OldPasswordHash";

            var rowAffected = await conn.ExecuteAsync(sql, new
            {
                NewPasswordHash = newPasswordHash,
                Email = email,
                OldPasswordHash = oldPasswordHash,
                
            },tx);

            return rowAffected > 0;
        } 
        #endregion

        #region DesactivateUserAsync
        public Task<bool> DeactivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region DeleteUserAsync
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            using var connection = await _connection.CreateConnectionAsync();
            const string command = @"DELETE FROM Users WHERE Id=@Id";

            var row = await connection.ExecuteAsync(command, new { Id = id });

            return row > 0;

        } 
        #endregion



        // queries


        #region GetAllUserAsync

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var connection = await _connection.CreateConnectionAsync();
            var query = @"
                    SELECT Id, Email, UserRef, MainRoleId, IsActive, CreatedAt, UpdatedAt  
                    FROM Users";
            var users = await connection.QueryAsync<User>(query);
            return users;
        }

        #endregion
        
        #region GetUserByEmailAsync
        public async Task<User?> GetUserByEmailAsync(string email,IDbConnection conn, IDbTransaction tx)
        {
   
            var query = @"SELECT TOP(1) * FROM Users WHERE Email = @Email";
            var user = await conn.QueryFirstOrDefaultAsync<User>(query, new { Email = email },tx);
            return user;
        }
        #endregion
        
        #region GetUserByEmailAsync Simple

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var connection = await _connection.CreateConnectionAsync();
            var query = @"SELECT TOP(1) * FROM Users WHERE Email = @Email";
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
            return user;
        }

        #endregion
        
        #region GetUserByEntityIdAs
        public async Task<User?> GetUserByEntityIdAsync(Guid entityId)
        {
            var connection = await _connection.CreateConnectionAsync();
            var query = @"SELECT TOP(1) * FROM Users WHERE EntityId = @EntityId";
            var user = await connection.ExecuteScalarAsync<User?>(query, new { entityId });
            return user;
        } 
        #endregion

        #region GetUserById 
        public async Task<User?> GetUserByIdAsync(Guid id, IDbConnection conn,IDbTransaction tx)
        {

            var query = @"
            SELECT 
                Id,
                Email,
                PasswordHash,
                UserRef,
                EntityId,
                MainRoleId,
                IsActive,
                CreatedAt,
                UpdatedAt
            FROM Users WHERE Id = @ID";
            var user = await conn.QueryFirstOrDefaultAsync<User>(query, new { id } , tx);
            return user;
        }
        #endregion

        #region GetUserByRefreshTokenAsync
        public Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetUserByUserRefAsync
        public Task<User?> GetUserByUserRefAsync(string userRef)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SearchUsersAsyn
        public Task<IEnumerable<User>> SearchUsersAsync(string searchTerm, int limit = 50)
        {
            throw new NotImplementedException();
        }

       
        #endregion
       
        #region UpdateRefreshTokenAsync
        public Task<bool> UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region UpdateUserAsync
        public async Task<bool> UpdateUserAsync(User user)
        {
            using var connection = await _connection.CreateConnectionAsync();
            const string command = @"
                UPDATE Users 
                SET 
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    UserRef = @UserRef,
                    EntityId = @EntityId,
                    MainRoleId = @MainRoleId,
                    IsActive = @IsActive,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id";

            var result = await connection.ExecuteAsync(command, user);
            return result == 1;

        }
        #endregion

        #region ExistsByEmailAsync

        public async Task<bool> ExistsByEmailAsync(string email)
        {

            using var connection = await _connection.CreateConnectionAsync();
            var sql = @"SELECT COUNT(1) FROM Users WHERE Email = @Email";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { email });
            return count > 0;

        }

        #endregion

        #region ExistsByUserRefAsync

        public async Task<bool> ExistsByUserRefAsync(string userRef)
        {

            using var connection = await _connection.CreateConnectionAsync();
            var sql = @"SELECT COUNT(1) FROM Users WHERE UserRef = @UserRef";
            var user = await connection.ExecuteScalarAsync<int>(sql, new { userRef });
            return user > 1;
        }
        #endregion




    }
}
