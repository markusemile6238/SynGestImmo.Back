using Entity.Service.Application.Common;
using Entity.Service.Application.Dtos.EntitySgi;
using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
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

        #region GetEntityById
        public async Task<CqsResult<EntitySgiDtos>> GetEntityByIdQuery(Guid id, IDbConnection conn, IDbTransaction tx)
        {
            Console.WriteLine("=====> dans EntityService");

            try
            {

                EntitySgi? entityDatas = await _entityQueryRepository.GetEntityByIdAsync(id,conn,tx);

                     Console.WriteLine("=====> After entityDatas"); 

                if(entityDatas == null)
                    return CqsResult<EntitySgiDtos>.Failure(Error.NotFound($"Entity profile must be create "));

                Person? personDatas = await _personQueryRepository.GetPersonByIdAsync(id, conn, tx);

                     Console.WriteLine("=====> After personDatas"); 

                if(personDatas == null)
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
                    CreatedAt = entityDatas.CreatedAt,
                    UpdatedAt = entityDatas.UpdatedAt
                };

                     Console.WriteLine("=====> After pofil creation"); 

                return CqsResult<EntitySgiDtos>.Success(profil);

            }catch(SqlException ex)
            {
                var error = _sqlException.Translate(ex);
                return CqsResult<EntitySgiDtos>.Failure(Error.Database(error.Message));

            }catch(Exception ex)
            {
                return CqsResult<EntitySgiDtos>.Failure(Error.Unknown(ex.Message));
            }
        }

        #endregion

    }



}
