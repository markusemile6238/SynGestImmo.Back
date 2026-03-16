namespace Entity.Service.Domain.Entities
{
    public class Tenant
    {
        public Guid EntityId { get; set; }
        public DateTime MoveIndate { get; set; }
        public DateTime MoveOutDate { get; set; }
    }
}
