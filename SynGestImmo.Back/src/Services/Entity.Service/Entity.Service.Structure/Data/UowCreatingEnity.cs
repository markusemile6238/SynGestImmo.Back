using Entity.Service.Application.common;
using System.Data;

namespace Entity.Service.Structure.Data
{
    public class UowCreatingEnity : IUowCreateEnity
    {

        private readonly IDapperConnection _connection;

        public UowCreatingEnity(IDapperConnection connection)
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

        public async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> action)
        {
            using var connection = await _connection.CreateConnectionAsync();
            if (connection.State != ConnectionState.Open) connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                var result = await action(connection, transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
    }
}
