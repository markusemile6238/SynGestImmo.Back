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

        public async Task<bool> AssignRoleToUserAsync(Guid userId, int roleId,IDbConnection? connection = null, IDbTransaction? transaction = null)
        {

            bool createdNew = false;
            if(connection == null)
            {
                connection = await _connection.CreateConnectionAsync();
                createdNew = true;
            }

            string command = @"INSERT INTO UserRoles (UserId,RoleId) VALUES (@userId,@roleId)";

            var row = await connection.ExecuteAsync(command, new { userId, roleId }, transaction);
            
            if (createdNew) connection.Dispose();

            return row > 0;
        }

        public async Task<bool> DeleteUserRoleAsync(Guid userId)
        {
            using var connection = await _connection.CreateConnectionAsync();
            string command = @"DELETE FROM UserRoles WHERE UserId = @userId";

            var row = await connection.ExecuteAsync(command, new {userId});

            return row > 0;
        }

        public Task<IEnumerable<User>> GetAllUserIdByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesOfUserId(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
