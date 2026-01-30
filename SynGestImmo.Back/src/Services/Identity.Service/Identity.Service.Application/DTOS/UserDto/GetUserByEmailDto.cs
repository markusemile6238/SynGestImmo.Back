using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.DTOS.UserDto
{
    public class GetUserByEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
