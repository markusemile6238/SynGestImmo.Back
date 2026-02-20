using Identity.Service.Application.Features.UserFeature.Commands.CreateUser;
using System.Data;
using Tools.Result;

namespace Identity.Service.Application.Common
{
    public interface IUserCreator
    {
        Task<CqsResult> CreateUserAsync (CreateUserCommand request, IDbConnection conn, IDbTransaction tx);
    }
}
