using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
using System.Data;
using Tools.Result;

namespace Entity.Service.Application.Common
{
    public interface IEntityService
    {
        Task<CqsResult> CreateNewEntity(CreateEntityCommand request, IDbConnection conn, IDbTransaction tx);
    }
}
