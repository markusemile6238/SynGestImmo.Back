using System.Data;

namespace Entity.Service.Application.Common{

    public interface IDapperConnection
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
