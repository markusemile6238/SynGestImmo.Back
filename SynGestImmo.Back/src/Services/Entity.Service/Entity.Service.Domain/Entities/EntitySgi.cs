using Entity.Service.Domain.Enum;

namespace Entity.Service.Domain.Entities
{
    public class EntitySgi
    {
        public Guid Id { get; set; }
        public EntityTypeEnum  EntityType { get; set; }
        public string DisplayName { get; set; } 
        public string Email { get; set; } 
        public string Phone { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
