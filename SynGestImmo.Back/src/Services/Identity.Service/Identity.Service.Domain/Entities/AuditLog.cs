using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }  // Utilisateur qui a fait l'action
        public string Action { get; set; } = string.Empty; // CREATE, UPDATE, DELETE, LOGIN, etc.
        public string EntityType { get; set; } = string.Empty; // User, Role, etc.
        public Guid? EntityId { get; set; }  // ID de l'entité modifiée
        public string? OldValues { get; set; } // JSON des anciennes valeurs
        public string? NewValues { get; set; } // JSON des nouvelles valeurs
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}
