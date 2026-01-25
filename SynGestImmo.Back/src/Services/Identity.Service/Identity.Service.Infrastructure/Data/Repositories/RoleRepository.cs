using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Infrastructure.Handlers;
using Identity.Service.Infrastructure.Validators;
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
        private readonly RoleFieldsValidator _roleValidator;

        public RoleRepository(IDapperConnection connection, ILogger<RoleRepository> logger, SqlExceptionsHandler exceptionsHandler, RoleFieldsValidator roleValidator)
        {
            _connection = connection;
            _logger = logger;
            _sqlHandler = exceptionsHandler;
            _roleValidator = roleValidator;
        }

        public Task<CqsResult> AssignRoleToUserAsync(Guid assignedBy, Guid assignedAt, int role)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public CqsResult<IEnumerable<Role>> GetAllRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<Role?>> GetRoleByIdAsync(int id)
        {

            throw new NotImplementedException();
        }

        #region IsRoleExistAsync
        public async Task<CqsResult<bool>> IsRoleExistAsync(int id)
        {
            try
            {

                using var connection = await _connection.CreateConnectionAsync();
                var sql = @"SELECT COUNT(1) FROM Roles WHERE Id = @Id";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { id });
                return CqsResult<bool>.Success(count > 0);

            }
            catch (SqlException ex)
            {
                _logger.LogError($"Sql Error : {ex.Message}\n Sql Code Number {ex.Number}");
                return _sqlHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected Error : Unable to verify role existence: {ex.Message}");
                return Error.Database("Unable to verify role existence");
            }

        }

        #endregion

        public Task<CqsResult> RemoveRoleFromeUserAsync(Guid assignedBy, Guid assignedAt, int role)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> UpdateRoleAsync(int id, Role role)
        {
            throw new NotImplementedException();
        }
    }
}
