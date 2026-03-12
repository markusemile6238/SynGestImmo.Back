using Entity.Service.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entity.Service.Application.Dtos.EntitySgi
{
    public class CreateEntityDtos
    {
        [Required]
        public Guid? Id { get; set; }

        [Required]
        [EnumDataType(typeof(EntityTypeEnum))]
        public EntityTypeEnum? EntityType { get; set; } 

        [Required]
        public string DisplayName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{7,14}$", ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; } 

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; } 

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string NationalId { get; set; } 
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
