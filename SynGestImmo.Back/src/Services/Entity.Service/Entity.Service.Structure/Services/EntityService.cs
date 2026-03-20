using Entity.Service.Application.Common;
using Entity.Service.Application.Dtos.EntitySgi;
using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
using Entity.Service.Application.Features.EntityFeature.Commands.UpdateEntity;
using Entity.Service.Domain.Entities;
using Entity.Service.Domain.Repositories.EntityRepositories;
using Entity.Service.Domain.Repositories.PersonRepositories;
using Microsoft.Data.SqlClient;
using System.Data;
using Tools.Result;


namespace Entity.Service.Structure.Services
{
    public class EntityService(
        IEntityCommandRepository _entityCommandRepository,
        IEntityQueryRepository _entityQueryRepository,
        IPersonCommandRepository _personCommandRepository,
        IPersonQueryRepository _personQueryRepository,
        ISqlExceptionTranslator _sqlException
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
                    EntityType = (int)request.EntityType,
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
                    NationalId = request.NationalId,
                    JobTitle = request.JobTitle
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

        #region UpdateEntityAsync
        public async Task<CqsResult> UpdateEntity(UpdateEntityCommand request, IDbConnection conn, IDbTransaction tx)
        {
            if (request == null)
                return CqsResult.Failure(Error.Validation("Request cannot be null"));

                Console.WriteLine($"=================> {request.EntityId}");
            

            try
            {


                // on va d'bord vérifier si l'entité existe et le charger 'Entity' et 'Person'
               var entityDatas = _entityQueryRepository.GetEntityByIdAsync(request.EntityId, conn, tx);
               var personDatas = _personQueryRepository.GetPersonByIdAsync(request.EntityId, conn, tx);

                await Task.WhenAll(entityDatas, personDatas);

                EntitySgi? currentEntity = entityDatas.Result;
                Person? currentPerson = personDatas.Result;

                if (currentEntity == null)
                    return CqsResult.Failure(Error.NotFound($"Entity with id {request.EntityId} not found"));


                if (currentPerson == null)
                    return CqsResult.Failure(Error.NotFound($"Person profile for entity with id {request.EntityId} not found"));

                // mapping des entités avec les données

                currentEntity.Id = request.EntityId;
                currentEntity.DisplayName = (request.DisplayName == null) ? currentEntity.DisplayName : request.DisplayName;
                currentEntity.Email = (request.Email == null) ? currentEntity.Email : request.Email;
                currentEntity.Phone = (request.Phone == null) ? currentEntity.Phone : request.Phone;
                currentEntity.IsActive = (request.IsActive == null) ? currentEntity.IsActive : request.IsActive.Value;
                currentPerson.EntityId = request.EntityId;
                currentPerson.FirstName = (request.FirstName == null) ? currentPerson.FirstName : request.FirstName;
                currentPerson.LastName = (request.LastName == null) ? currentPerson.LastName : request.LastName;
                currentPerson.BirthDate = (request.BirthDate == null) ? currentPerson.BirthDate : request.BirthDate;
                currentPerson.NationalId = (request.NationalId == null) ? currentPerson.NationalId : request.NationalId;
                currentPerson.JobTitle = (request.JobTitle == null) ? currentPerson.JobTitle : request.JobTitle;

                // mise a jour des entity
                await _entityCommandRepository.UpdateEntityAsync(currentEntity, conn, tx);
                await _personCommandRepository.UpdatePersonAsync(currentPerson, conn, tx);

                return CqsResult.Success("Entity updated successfully");
            }
            catch (SqlException ex)
            {
                var error = _sqlException.Translate(ex);
                return CqsResult.Failure(Error.Database(error.Message));
            }
            catch (Exception ex)
            {
                return CqsResult.Failure(Error.Unknown($"Error updating entity {ex.Message}"));
            }

        }

        #endregion

        #region GetEntityById
        public async Task<CqsResult<EntitySgiDtos>> GetEntityByIdQuery(Guid id, IDbConnection conn, IDbTransaction tx)
        {
            try
            {

                EntitySgi? entityDatas = await _entityQueryRepository.GetEntityByIdAsync(id, conn, tx);

                if (entityDatas == null)
                    return CqsResult<EntitySgiDtos>.Failure(Error.NotFound($"Entity profile must be create "));

                Person? personDatas = await _personQueryRepository.GetPersonByIdAsync(id, conn, tx);

                if (personDatas == null)
                    return CqsResult<EntitySgiDtos>.Failure(Error.NotFound("Person profile must be create"));

                EntitySgiDtos profil = new EntitySgiDtos
                {
                    Id = entityDatas.Id,
                    EntityType = entityDatas.EntityType,
                    DisplayName = entityDatas.DisplayName,
                    LastName = personDatas.LastName,
                    FirstName = personDatas.FirstName,
                    BirthDate = personDatas.BirthDate,
                    Email = entityDatas.Email,
                    Phone = entityDatas.Phone,
                    NationalId = personDatas.NationalId,
                    IsActive = entityDatas.IsActive,
                    JobTitle = personDatas.JobTitle,
                    CreatedAt = entityDatas.CreatedAt,
                    UpdatedAt = entityDatas.UpdatedAt
                };


                return CqsResult<EntitySgiDtos>.Success(profil);

            }
            catch (SqlException ex)
            {
                var error = _sqlException.Translate(ex);
                return CqsResult<EntitySgiDtos>.Failure(Error.Database(error.Message));

            }
            catch (Exception ex)
            {
                return CqsResult<EntitySgiDtos>.Failure(Error.Unknown(ex.Message));
            }
        }
        #endregion

    }



}
