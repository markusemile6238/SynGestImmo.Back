using Entity.Service.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.Application.Dtos.EntitySgi
{
    public class EntitySgiDtos
    {
        public Guid Id { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string NationalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
