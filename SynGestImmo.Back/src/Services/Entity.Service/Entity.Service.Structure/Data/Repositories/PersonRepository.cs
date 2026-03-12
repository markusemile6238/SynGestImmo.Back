using Dapper;
using Entity.Service.Domain.Entities;
using Entity.Service.Domain.ExceptionService;
using Entity.Service.Domain.Repositories.PersonRepositories;
using System.Data;

namespace Entity.Service.Structure.Data.Repositories
{
    public class PersonRepository(
        ) : IPersonCommandRepository, IPersonQueryRepository
    {


        public async Task<Guid> CreatePersonAsync(Person person, IDbConnection conn, IDbTransaction tx)
        {
           const string sql = @"INSERT INTO [entity].[Persons](
                                EntityId,
                                FirstName,
                                LastName,
                                BirthDate,
                                NationalId) 
                            VALUES (@EntityId, @FirstName, @LastName, @BirthDate, @NationalId);
                            SELECT CAST(SCOPE_IDENTITY() as UNIQUEIDENTIFIER) ";
           
            Guid entityId = await conn.ExecuteScalarAsync<Guid>(sql, person, tx);
            if (entityId == Guid.Empty)
                throw new EntityServiceExceptions("Insert Person Failed");

            return entityId;


        }

        public Task<bool> DeletePersonAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetEntityByLastnameOrFirstname(string term, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        public Task<EntitySgi?> GetPersonByIdAsync(int id, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePersonAsync(Person person, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }
    }
}
