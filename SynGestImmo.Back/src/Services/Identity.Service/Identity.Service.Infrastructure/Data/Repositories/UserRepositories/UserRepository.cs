using Identity.Service.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories.UserRepositories
{
    public class UserRepository : IUserCommandRepository, IUserQueryRepository
    {
        public Task<CqsResult> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> DeactivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<bool>> ExistsByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<bool>> ExistsByUserRefAsync(string userRef)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByEntityIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<User>> GetUserByUserRefAsync(string userRef)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult<IEnumerable<User>>> SearchUsersAsync(string searchTerm, int limit = 50)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> UpdateRefreshTokenAsync(Guid userId, string? refreshToken, DateTime? expiry)
        {
            throw new NotImplementedException();
        }

        public Task<CqsResult> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
