using System.Data;

namespace Identity.Service.Infrastructure.Data
{
    public interface IDapperConnection
    {
            Task<IDbConnection> CreateConnectionAsync();
    }
}
