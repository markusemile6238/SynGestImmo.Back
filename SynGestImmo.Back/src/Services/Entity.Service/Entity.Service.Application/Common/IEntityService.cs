using Entity.Service.Application.Dtos.EntitySgi;
using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
using System.Data;
using Tools.Result;

namespace Entity.Service.Application.Common
{
    public interface IEntityService
    {
        Task<CqsResult> CreateNewEntity(CreateEntityCommand request, IDbConnection conn, IDbTransaction tx);
        Task<CqsResult<EntitySgiDtos>> GetEntityByIdQuery(Guid id, IDbConnection conn, IDbTransaction tx);
    }
}
