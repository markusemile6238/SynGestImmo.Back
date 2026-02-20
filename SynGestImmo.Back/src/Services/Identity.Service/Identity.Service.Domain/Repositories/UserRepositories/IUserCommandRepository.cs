using Identity.Service.Domaine.Entities;
using System.Data;
using Tools.Result;

namespace Identity.Service.Domain.Repositories.UserRepositories
{
    public interface IUserCommandRepository
    {
        //Commands
        Task<Guid> CreateUserAsync(User user, IDbConnection connection, IDbTransaction transaction);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user,IDbConnection conn, IDbTransaction tx);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> AssignRoleIdAsync(int id, Guid userId);
        Task<bool> UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry);
        Task<bool> DeactivateUserAsync(Guid userId);
        Task<bool> ActivateUserAsync(Guid userId);
        Task<bool> ChangePassword(string oldPasswordHash, string newPasswordHash, string email,IDbConnection conn,IDbTransaction tx);

    }
}
