using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserRoleFeature.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommand : IRequest<CqsResult>
    {
        public Guid Id { get; set; }
        public int roleId { get; set; } = 0;
    }
}
