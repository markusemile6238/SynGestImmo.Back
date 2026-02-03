using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Exceptions;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Domaine.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    class UserRolesRepository : IUserRolesCommandRepository, IUserRolesQueryRepository
    {
        private readonly IDapperConnection _connection;
        private readonly ILogger<UserRolesRepository> _logger;

        public UserRolesRepository(IDapperConnection connection, ILogger<UserRolesRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        // COMMANDS


        #region AssignRoleToUserAsync
        public async Task<bool> AssignRoleToUserAsync(Guid userId, int roleId, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {

            bool createdNew = false;
            if (connection == null)
            {
                connection = await _connection.CreateConnectionAsync();
                createdNew = true;
            }

            string command = @"INSERT INTO UserRoles (UserId,RoleId) VALUES (@userId,@roleId)";

            var row = await connection.ExecuteAsync(command, new { userId, roleId }, transaction);

            if (createdNew) connection.Dispose();

            return row > 0;
        }
        #endregion

        #region DeleteUserRoleAsync
        public async Task<bool> DeleteUserRoleAsync(Guid userId)
        {
            using var connection = await _connection.CreateConnectionAsync();
            string command = @"DELETE FROM UserRoles WHERE UserId = @userId";

            var row = await connection.ExecuteAsync(command, new { userId });

            return row > 0;
        }
        #endregion



        // QUERIES

        #region  GetAllUserIdByRoleIdAsync
        public async Task<IEnumerable<User>> GetAllUserIdByRoleIdAsync(int roleId)
        {
            using var connection = await _connection.CreateConnectionAsync();

            const string sql = @"
            SELECT 
                u.Id,
                u.Email,
                u.PasswordHash,
                u.UserRef,
                u.EntityId,
                u.MainRoleId,
                u.IsActive,
                u.CreatedAt,
                u.UpdatedAt
            FROM UserRoles ur
            INNER JOIN Users u ON u.Id = ur.UserId
            WHERE ur.RoleId = @roleId
              AND u.IsActive = 1;
        ";

            return await connection.QueryAsync<User>(
                sql,
                new { roleId }
            );
        }
        #endregion

        #region  GetRolesOfUserIdAsyn
        public async Task<IEnumerable<Role>> GetRolesOfUserIdAsync(Guid id)
        {
            using var connection = await _connection.CreateConnectionAsync();

            const string sql = @"
                SELECT 
                    r.Id,
                    r.Name,
                    r.Description,
                    r.IsSystemRole,
                    r.IsActive,
                    r.CreatedAt,
                    r.UpdatedAt
                FROM UserRoles ur
                INNER JOIN Roles r ON r.Id = ur.RoleId
                WHERE ur.UserId = @userId
                  AND r.IsActive = 1;
            ";

            return await connection.QueryAsync<Role>(
                sql,
                new { userId = id }
            );
        } 
        #endregion



    }
}
