using Identity.Service.Domaine.Entities;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories.UserRepositories

{
    public interface IUserQueryRepository
    {
        //Queries
        Task<CqsResult<User>> GetUserByIdAsync(Guid id);
        Task<CqsResult<User>> GetUserByEmailAsync(string email);
        Task<CqsResult<User>> GetUserByUserRefAsync(string userRef);
        Task<CqsResult<User>> GetUserByEntityIdAsync(Guid entityId);
        Task<CqsResult<User>> GetUserByRefreshTokenAsync(string refreshToken);
        Task<CqsResult<IEnumerable<User>>> SearchUsersAsync(string searchTerm, int limit = 50);
        Task<CqsResult<bool>> ExistsByEmailAsync(string email);
        Task<CqsResult<bool>> ExistsByUserRefAsync(string userRef);



    }
}
