/**
 *  UserRef : utiliser pour relation utile dans la communication
 *  EntityId : utiliser pour relation metier
 **/

namespace Identity.Service.Domaine.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string UserRef { get; set; } = string.Empty;
        public Guid? EntityId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public int? MainRoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
