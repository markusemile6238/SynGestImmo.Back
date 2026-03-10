using System.Data;

namespace Entity.Service.Structure.Data
{
    public interface IDapperConnection
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
