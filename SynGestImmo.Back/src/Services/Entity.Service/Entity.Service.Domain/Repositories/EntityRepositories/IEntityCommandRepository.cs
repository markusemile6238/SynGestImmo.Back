using Entity.Service.Domain.Entities;
using System.Data;
using Tools.Result;


namespace Entity.Service.Domain.Repositories.EntityRepositories
{
    public interface IEntityCommandRepository
    {
        Task<Guid> CreateEntityAsync(EntitySgi entity, IDbConnection conn, IDbTransaction tx);
        Task<bool> UpdateEntityAsync(EntitySgi entity, IDbConnection conn, IDbTransaction tx);
        Task<bool> DeleteEntityAsync(int id);
        Task<bool> DesactiveEntityAsync(int id);
        Task<bool> ActiveEntityAsync(int id);

    }
}
