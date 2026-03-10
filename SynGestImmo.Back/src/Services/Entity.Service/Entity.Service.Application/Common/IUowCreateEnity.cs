using System.Data;

namespace Entity.Service.Application.common
{
    public interface IUowCreateEnity
    {
        Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> action);
        Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> action);

    }
}
