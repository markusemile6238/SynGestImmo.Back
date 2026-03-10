using Entity.Service.Domain.Enum;

namespace Entity.Service.Domain.Entities
{
    public class EntityAddress
    {
        public int EntityId { get; set; }
        public int AddressId { get; set; }
        public AddressTypeEnum AddressType { get; set; } = AddressTypeEnum.Billing;
    }
}
