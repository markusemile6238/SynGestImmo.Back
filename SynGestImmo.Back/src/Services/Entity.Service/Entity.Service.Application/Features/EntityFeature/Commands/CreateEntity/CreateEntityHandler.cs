using Entity.Service.Application.common;
using Entity.Service.Application.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity
{
    public class CreateEntityHandler : IRequestHandler<CreateEntityCommand, CqsResult>
    {

        private readonly IUowCreateEnity _uowCreateEnity;
        private readonly ILogger<CreateEntityHandler> _logger;
        private readonly ISqlExceptionTranslator _sqlExceptionTranslator;

        public CreateEntityHandler(IUowCreateEnity uowCreateEnity, ILogger<CreateEntityHandler> logger, ISqlExceptionTranslator sqlExceptionTranslator)
        {
            _uowCreateEnity = uowCreateEnity;
            _logger = logger;
            _sqlExceptionTranslator = sqlExceptionTranslator;
        }

        public Task<CqsResult> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
