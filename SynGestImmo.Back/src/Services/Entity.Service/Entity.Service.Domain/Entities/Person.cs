using Entity.Service.Domain.Enum;

namespace Entity.Service.Domain.Entities
{
    public class Person
    {
        public Guid EntityId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime? BirthDate {  get; set; }
        public string NationalId { get; set; } = string.Empty;
        public JobTitleEnum? JobTitle { get; set; }
    }
}
