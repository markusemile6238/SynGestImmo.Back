using Entity.Service.Domain.Entities;
using System.Data;
using Tools.Result;


namespace Entity.Service.Domain.Repositories.EntityRepositorie
{
    public interface IEntityCommandRepository
    {
        Task<bool> CreateEntityAsync(EntitySgi entity, IDbConnection conn, IDbTransaction tx);
        Task<bool> UpdateEntityAsync(EntitySgi entity, IDbConnection conn, IDbTransaction tx);
        Task<bool> DeleteEntityAsync(int id);
        Task<bool> DesactiveEntityAsync(int id);
        Task<bool> ActiveEntityAsync(int id);

    }
}
