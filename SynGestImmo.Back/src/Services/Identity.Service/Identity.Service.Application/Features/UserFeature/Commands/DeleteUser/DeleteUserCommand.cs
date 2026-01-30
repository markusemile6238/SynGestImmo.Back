using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<CqsResult>
    {
        public Guid Id { get; set; }
    }
}
