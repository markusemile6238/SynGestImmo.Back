using System.Data;

namespace Entity.Service.Application.Common
{
    public interface IUowCreateEntity
    {
        Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> action);
        Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> action);

    }
}
