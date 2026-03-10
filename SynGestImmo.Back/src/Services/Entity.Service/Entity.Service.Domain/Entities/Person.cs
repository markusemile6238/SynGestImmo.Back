using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.Domain.Entities
{
    public class Person
    {
        public int EntityId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime BirthDate {  get; set; }
        public string nationalId { get; set; } = string.Empty;
    }
}
