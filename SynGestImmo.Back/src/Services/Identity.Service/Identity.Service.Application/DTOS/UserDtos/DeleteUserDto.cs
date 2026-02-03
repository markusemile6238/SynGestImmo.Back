using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.DTOS.UserDtos
{
    public class DeleteUserDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
