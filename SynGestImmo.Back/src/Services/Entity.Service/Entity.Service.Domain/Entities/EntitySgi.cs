using Entity.Service.Domain.Enum;

namespace Entity.Service.Domain.Entities
{
    public class EntitySgi
    {
        public int Id { get; set; }
        public EntityTypeEnum  EntityType { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

    }
}
