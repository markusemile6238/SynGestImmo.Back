using Identity.Service.Domaine.Entities;
using System.Data;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories.UserRepositories

{
    public interface IUserQueryRepository
    {
        //Queries

        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetUserByIdAsync(Guid id, IDbConnection conn, IDbTransaction tx);
        Task<User?> GetUserByEmailAsync(string email, IDbConnection conn, IDbTransaction tx);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUserRefAsync(string userRef);
        Task<User?> GetUserByEntityIdAsync(Guid entityId);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm, int limit = 50);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUserRefAsync(string userRef);




    }
}
