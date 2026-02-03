using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Exceptions;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Infrastructure.Handlers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    public class RoleRepository : IRolesCommandRepository, IRolesQueryRepository
    {

        private readonly IDapperConnection _connection;
        private readonly ILogger<RoleRepository> _logger;
        private readonly SqlExceptionsHandler _sqlHandler;


        public RoleRepository(IDapperConnection connection, ILogger<RoleRepository> logger, SqlExceptionsHandler exceptionsHandler)
        {
            _connection = connection;
            _logger = logger;
            _sqlHandler = exceptionsHandler;

        }

        public Task AssignRoleToUserAsync(Guid assignedBy, Guid assignedAt, int role)
        {
            throw new NotImplementedException();
        }

        public Task CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region GetAllRoleAsync
        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            using var connection = await _connection.CreateConnectionAsync();
            const string sql = @"
                SELECT 
                    Id,
                    Name,
                    Description,
                    IsSystemRole,
                    IsActive,
                    Prefixe,
                    CreatedAt,
                    UpdatedAt 
                FROM Roles 
                WHERE IsActive=1";

            var roles = await connection.QueryAsync<Role>(sql);
            return roles;
        } 
        #endregion

        #region GetRoleByIdAsynch
        public async Task<Role?> GetRoleByIdAsync(int id)
        {

            using var connection = await _connection.CreateConnectionAsync();

            var sql = @"SELECT Id,Name,Description,IsSystemRole,IsActive,Prefixe,CreatedAt,UpdatedAt FROM Roles WHERE Id=@Id AND IsActive=1";

            var role = await connection.QueryFirstOrDefaultAsync<Role>(sql, new { id });

            return role;
        } 
        #endregion

        #region IsRoleExistAsync
        public async Task<bool> IsRoleExistAsync(int id)
        {
            using var connection = await _connection.CreateConnectionAsync();
            var sql = @"SELECT COUNT(1) FROM Roles WHERE Id = @Id";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { id });
            return count > 0;

        }

        #endregion

        public Task RemoveRoleFromeUserAsync(Guid assignedBy, Guid assignedAt, int role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoleAsync(int id, Role role)
        {
            throw new NotImplementedException();
        }
    }
}
