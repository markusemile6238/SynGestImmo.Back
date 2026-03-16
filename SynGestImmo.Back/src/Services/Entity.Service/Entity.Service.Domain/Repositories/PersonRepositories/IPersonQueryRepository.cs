using Entity.Service.Domain.Entities;
using System.Data;

namespace Entity.Service.Domain.Repositories.PersonRepositories
{
    public interface IPersonQueryRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person?> GetPersonByIdAsync(Guid id, IDbConnection conn, IDbTransaction tx);
        Task<IEnumerable<Person>> GetEntityByLastnameOrFirstname(string term, IDbConnection conn, IDbTransaction tx);
    }
}
