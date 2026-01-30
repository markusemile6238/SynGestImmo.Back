using Identity.Service.Domaine.Entities;
using System.Data;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories.UserRepositories
{
    public interface IUserCommandRepository
    {
        //Commands
        Task<Guid> CreateUserAsync(User user, IDbConnection connection, IDbTransaction transaction);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> AssignRoleIdAsync(int id, Guid userId);
        Task<bool> UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry);
        Task<bool> DeactivateUserAsync(Guid userId);
        Task<bool> ActivateUserAsync(Guid userId);

    }
}
