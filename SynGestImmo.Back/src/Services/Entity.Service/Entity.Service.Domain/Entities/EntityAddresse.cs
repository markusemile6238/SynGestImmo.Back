using Entity.Service.Domain.Enum;

namespace Entity.Service.Domain.Entities
{
    public class EntityAddress
    {
        public Guid EntityId { get; set; }
        public int AddressId { get; set; }
        public AddressTypeEnum AddressType { get; set; } = AddressTypeEnum.Billing;
    }
}
