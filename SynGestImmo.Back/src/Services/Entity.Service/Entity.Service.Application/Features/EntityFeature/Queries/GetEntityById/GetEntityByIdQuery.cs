using Entity.Service.Application.Dtos.EntitySgi;
using MediatR;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Queries.GetEntityById
{
    public class GetEntityByIdQuery : IRequest<CqsResult<EntitySgiDtos>>
    {
        public Guid? EntityId { get; set; }    
    }
}
