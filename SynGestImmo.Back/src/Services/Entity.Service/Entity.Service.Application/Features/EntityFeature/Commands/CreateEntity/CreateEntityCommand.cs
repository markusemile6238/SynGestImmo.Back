using Entity.Service.Domain.Enum;
using MediatR;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity
{
    public class CreateEntityCommand : IRequest<CqsResult>
    {
        public Guid Id { get; set; }
        public EntityTypeEnum EntityType { get; set; } 
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public JobTitleEnum? JobTitle {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       
    }


}
