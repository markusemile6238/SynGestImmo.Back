using Entity.Service.Domain.Enum;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Commands.UpdateEntity
{
    public class UpdateEntityCommand : IRequest<CqsResult>
    {
        public Guid EntityId { get; set; }
        public int? EntityType { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? NationalId { get; set; }
        public JobTitleEnum? JobTitle { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
