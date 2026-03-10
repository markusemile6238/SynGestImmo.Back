using Entity.Service.Domain.Enum;
using Entity.Service.Domain.Repositories.EntityRepositorie;
using System.Data;

namespace Entity.Service.Structure.Data.Repositories
{
    public class EntitySgiRepository : IEntityCommandRepository, IEntityQueryRepository
    {
        public Task<bool> ActiveEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateEntityAsync(Domain.Entities.EntitySgi entity, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DesactiveEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.EntitySgi>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.EntitySgi>> GetEntityByEntityType(EntityTypeEnum entityType, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.EntitySgi?> GetEntityByIdAsync(int id, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntityAsync(Domain.Entities.EntitySgi entity, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }
    }
}
