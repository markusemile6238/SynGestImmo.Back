using Identity.Service.Domaine.Entities;
using MediatR;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<CqsResult<User?>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
