using Identity.Service.Application.Common;
using System.Data;

namespace Identity.Service.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDapperConnection _connection;

        public UnitOfWork(IDapperConnection connection)
        {
            _connection = connection;
        }

        public async Task ExecuteAsync(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using var connection = await _connection.CreateConnectionAsync();

            if (connection.State != ConnectionState.Open) connection.Open();
           
            using var transaction = connection.BeginTransaction();

            try
            {                
                await action(connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
