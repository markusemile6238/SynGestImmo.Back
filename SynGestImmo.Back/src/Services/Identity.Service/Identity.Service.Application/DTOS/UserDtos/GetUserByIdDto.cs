using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.DTOS.UserDtos
{
    public class GetUserByIdDto
    {
        [Required]
        public string Id { get; set; }
    }
}
