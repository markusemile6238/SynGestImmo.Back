using Identity.Service.Domaine.Entities;

namespace Identity.Service.Application.DTOS.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserRef { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; }
        public Guid EntityId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }       

    }
}
