using Entity.Service.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entity.Service.Application.Dtos.EntitySgi
{
    public class UpdateEntityDtos
    {
        public Guid EntityId { get; set; }

        [EnumDataType(typeof(EntityTypeEnum))]
        public int? EntityType { get; set; }

        public string? DisplayName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [RegularExpression(@"^\+?[1-9]\d{7,14}$", ErrorMessage = "Invalid phone number format")]
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
