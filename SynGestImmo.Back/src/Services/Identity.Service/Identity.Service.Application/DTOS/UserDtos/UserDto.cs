using Identity.Service.Domaine.Entities;

namespace Identity.Service.Application.DTOS.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string UserRef { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsEmailConfirmed { get; set; }
        public Guid EntityId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

      
    }
}
