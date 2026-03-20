using Entity.Service.Application.Dtos.Api;
using Refit;
using Tools.Result;

namespace Entity.Service.Application.Features.IdentityFeature
{
    public interface IIdentityApi
    {
        [Get("/api/admin/user/detail/e/{id}")]
        Task<CqsResult<ApiIdentityGetUser>> UserExistAsync(Guid id);
    }
}
