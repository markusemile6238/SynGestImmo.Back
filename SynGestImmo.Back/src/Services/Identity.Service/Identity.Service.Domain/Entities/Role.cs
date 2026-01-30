using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Domain.Entities
{
    public class    Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Prefixe {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsSystemRole {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }


    }
}
