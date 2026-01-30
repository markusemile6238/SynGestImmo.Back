using Identity.Service.Domaine.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<CqsResult<User?>>
    {
        public string Email { get; set; } = string.Empty;
    }
}
