

using Identity.Service.Domaine.Entities;
using Tools.Result;

namespace Identity.Service.Domaine.Repositories
{
    public interface IUserRepository
    {
        //Commands
        CqsResult CreateUserAsync(User user);
        CqsResult UpdateUserAsync(User user);
        CqsResult DeleteUserAsync(User user);


        //Queries
        CqsResult<User> GetUserByIdAsync(Guid id);
        CqsResult<User> GetUserByUserRefAsync(string userRef);
        CqsResult<User> GetUserByEntityIdAsync(Guid entityId);



    }
}
