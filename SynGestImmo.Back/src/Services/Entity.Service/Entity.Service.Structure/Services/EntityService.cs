using Entity.Service.Application.Common;
using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
using Entity.Service.Domain.Entities;
using Entity.Service.Domain.Repositories.EntityRepositories;
using Entity.Service.Domain.Repositories.PersonRepositories;
using System.Data;
using Tools.Result;

namespace Entity.Service.Structure.Services
{
    public class EntityService(
        IEntityCommandRepository _entityCommandRepository,
        IEntityQueryRepository _entityQueryRepository,
        IPersonCommandRepository _personCommandRepository
        ) : IEntityService

    {


        #region CreateNewEntityAsync

        public async Task<CqsResult> CreateNewEntity(CreateEntityCommand request, IDbConnection conn, IDbTransaction tx)
        {
            try
            {
                if (request == null)
                    return CqsResult.Failure(Error.Validation("Request cannot be null"));
         

                EntitySgi newEntity = new EntitySgi
                {
                    Id = request.Id,
                    EntityType = request.EntityType,
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    Phone = request.Phone,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                Guid entityId = await _entityCommandRepository.CreateEntityAsync(newEntity, conn, tx);

                if (entityId == Guid.Empty)
                    return CqsResult.Failure(Error.Database("Failed to create entity in database"));

                Person newPerson = new Person
                {
                    EntityId = entityId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.BirthDate,
                    NationalId = request.NationalId
                };

                Guid personId = await _personCommandRepository.CreatePersonAsync(newPerson, conn, tx);

                if (personId == Guid.Empty)
                    return CqsResult.Failure(Error.Database("Failed to create Person in database"));


                return CqsResult.Success("Entity created successfully");
            }
            catch (Exception ex)
            {
                return CqsResult.Failure(Error.Unknown($"Error creating entity {ex.Message}"));
            }
        }

        #endregion



    }



}
