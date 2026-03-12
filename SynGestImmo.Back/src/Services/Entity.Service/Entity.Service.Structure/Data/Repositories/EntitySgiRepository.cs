using Dapper;
using Entity.Service.Domain.Entities;
using Entity.Service.Domain.Enum;
using Entity.Service.Domain.ExceptionService;
using Entity.Service.Domain.Repositories.EntityRepositories;
using System.Data;

namespace Entity.Service.Structure.Data.Repositories
{
    public class EntitySgiRepository(
        ) : IEntityCommandRepository, IEntityQueryRepository
    {
        
      

        public Task<bool> ActiveEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateEntityAsync(EntitySgi entity, IDbConnection conn, IDbTransaction tx)
        {
            const string sql = @"
                    INSERT INTO [entity].[Entities](
                        Id
                        EntityType,
                        DisplayName,
                        Email,
                        Phone,
                        IsActive,
                        CreatedAt) 
                    VALUES (@Id, @EntityType,@DisplayName,@Email,@Phone,@IsActive,@CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as UNIQUEIDENTIFIER)
                    ";

            Guid entityId = await conn.ExecuteScalarAsync<Guid>(sql, entity, tx);
            if (entityId == Guid.Empty)
                throw new EntityServiceExceptions("Insert Entity Failed");
            return entityId ;

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
