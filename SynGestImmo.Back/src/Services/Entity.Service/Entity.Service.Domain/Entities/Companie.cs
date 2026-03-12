namespace Entity.Service.Domain.Entities
{
    public class Companie
    {
        public Guid EntityId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string VatNumber { get; set; } = string.Empty;
        public string RegistrationNumber {  get; set; } = string.Empty;
    }
}
