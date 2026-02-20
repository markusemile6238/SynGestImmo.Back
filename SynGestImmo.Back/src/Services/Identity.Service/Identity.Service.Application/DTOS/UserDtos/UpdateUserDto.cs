namespace Identity.Service.Application.DTOS.UserDtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; } 
        public string? Username { get; set; } 
        public string? Email { get; set; }
        public bool? IsActive { get; set; } 
        public int? RoleId { get; set; } 
    }

   

}
