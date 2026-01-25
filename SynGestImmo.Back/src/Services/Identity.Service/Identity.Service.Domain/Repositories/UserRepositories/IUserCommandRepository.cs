using Identity.Service.Domaine.Entities;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories.UserRepositories
{
    public interface IUserCommandRepository
    {
        //Commands
        Task<CqsResult> CreateUserAsync(User user);
        Task<CqsResult> UpdateUserAsync(User user);
        Task<CqsResult> DeleteUserAsync(User user);
        Task<CqsResult> AssignRoleIdAsync(User user);
        Task<CqsResult> UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry);
        Task<CqsResult> DeactivateUserAsync(Guid userId);
        Task<CqsResult> ActivateUserAsync(Guid userId);

    }
}
