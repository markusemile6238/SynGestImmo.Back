using Entity.Service.Domain.Entities;
using System.Data;

namespace Entity.Service.Domain.Repositories.PersonRepositories
{
    public interface IPersonCommandRepository
    {
        Task<Guid> CreatePersonAsync(Person person, IDbConnection conn, IDbTransaction tx);
        Task<bool> UpdatePersonAsync(Person person, IDbConnection conn, IDbTransaction tx);
        Task<bool> DeletePersonAsync(int id);
    }
}
