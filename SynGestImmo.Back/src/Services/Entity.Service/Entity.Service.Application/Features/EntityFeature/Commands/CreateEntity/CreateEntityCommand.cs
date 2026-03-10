using MediatR;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity
{
    public class CreateEntityCommand : IRequest<CqsResult>
    {
        public string EntityType { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string nationalId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
