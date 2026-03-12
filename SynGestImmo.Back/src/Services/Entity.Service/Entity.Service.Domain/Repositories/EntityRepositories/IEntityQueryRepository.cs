using Entity.Service.Domain.Entities;
using Entity.Service.Domain.Enum;
using System.Data;

namespace Entity.Service.Domain.Repositories.EntityRepositories
{
    public interface IEntityQueryRepository
    {
        Task<IEnumerable<EntitySgi>> GetAll();
        Task<EntitySgi?> GetEntityByIdAsync(int id, IDbConnection conn, IDbTransaction tx);
        Task<IEnumerable<EntitySgi>> GetEntityByEntityType(EntityTypeEnum entityType, IDbConnection conn, IDbTransaction tx);
        


    }
}
