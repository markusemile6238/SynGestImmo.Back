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
                                [EntityId],
                                FirstName,
                                LastName,
                                BirthDate,
                                NationalId,
                                JobTitle) 
                            VALUES (@EntityId, @FirstName, @LastName, @BirthDate, @NationalId,@JobTitle)
                         ";
           
            int affetcedRows = await conn.ExecuteAsync(sql, person, tx);
            if (affetcedRows == 0)
                throw new EntityServiceExceptions("Insert Person Failed");

            return person.EntityId;


        }

        public Task<bool> DeletePersonAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> UpdatePersonAsync(Person person, IDbConnection conn, IDbTransaction tx)
        {
            const string sql = @"UPDATE [entity].[Persons]
                                SET FirstName = COALESCE(@FirstName, FirstName),
                                    LastName = COALESCE(@LastName,LastName),
                                    BirthDate = COALESCE(@BirthDate, BirthDate),
                                    NationalId = COALESCE(@NationalId, NationalId),
                                    JobTitle = COALESCE(@JobTitle, JobTitle)
                                WHERE EntityId = @EntityId";
            int affetcedRows = await conn.ExecuteAsync(sql, person, tx);
            return affetcedRows > 0;
        }
        
        
        // Query

        public Task<IEnumerable<Person>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetEntityByLastnameOrFirstname(string term, IDbConnection conn, IDbTransaction tx)
        {
            throw new NotImplementedException();
        }

        public async Task<Person?> GetPersonByIdAsync(Guid entityId, IDbConnection conn, IDbTransaction tx)
        {
            const string sql = @"SELECT 
                                    EntityId,FirstName,LastName,BirthDate,NationalId,JobTitle 
                                 FROM [entity].[Persons]
                                 WHERE EntityId = @EntityId";

            var person = await conn.QueryFirstOrDefaultAsync<Person?>(sql, new { EntityId =  entityId }, tx);
            return person;

        }

    }
}
