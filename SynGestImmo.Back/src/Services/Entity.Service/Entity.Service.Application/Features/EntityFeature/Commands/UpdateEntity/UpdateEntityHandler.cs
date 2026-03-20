using Entity.Service.Application.Common;
using Entity.Service.Application.Features.EntityFeature.Commands.CreateEntity;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Commands.UpdateEntity
{
    public class UpdateEntityHandler(
                       IUowCreateEntity _uowCreateEntity,
        ILogger<CreateEntityHandler> _logger,
        ISqlExceptionTranslator _sqlExceptionTranslator,
        IEntityService _entityService
                ) : IRequestHandler<UpdateEntityCommand, CqsResult>
    {


        public async Task<CqsResult> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
        {

            #region VALIDATIONS
            if (request == null)
                return CqsResult<bool>.Failure(Error.Validation("Request null ! Nothing to update"));
            if (request.EntityId == Guid.Empty)
                return CqsResult<bool>.Failure(Error.Validation("EntityId is required"));
            #endregion

            Console.WriteLine("=======> Application Update");

            try
            {

                var result = await _uowCreateEntity.ExecuteAsync(
                    (conn, tx) => _entityService.UpdateEntity(request, conn, tx));
                return result;

            }
            catch (SqlException ex)
            {
                var error = _sqlExceptionTranslator.Translate(ex);
                _logger.LogError(ex, "SQL Exception occurred while creating entity");
                return CqsResult.Failure(error);

            }
        }
    }
}
