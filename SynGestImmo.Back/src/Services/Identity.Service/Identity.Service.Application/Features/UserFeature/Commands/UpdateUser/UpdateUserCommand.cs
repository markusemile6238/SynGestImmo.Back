using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<CqsResult<bool>>
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? MainRoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool? MustChangePassword { get; set; } 

    }
}
