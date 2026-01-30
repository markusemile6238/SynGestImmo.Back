using System.Data;

namespace Identity.Service.Application.Common
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> action);
    }
}
