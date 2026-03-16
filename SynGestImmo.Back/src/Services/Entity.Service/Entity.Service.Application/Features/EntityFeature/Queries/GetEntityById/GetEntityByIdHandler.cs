using Entity.Service.Application.Common;
using Entity.Service.Application.Dtos.EntitySgi;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using Tools.Result;

namespace Entity.Service.Application.Features.EntityFeature.Queries.GetEntityById
{
    public class GetEntityByIdHandler(
        ILogger<GetEntityByIdHandler> logger,
        IEntityService entityService,
        ISqlExceptionTranslator sqlExceptionTranslator,
        IDapperConnection dapperConnection

        ) : IRequestHandler<GetEntityByIdQuery, CqsResult<EntitySgiDtos>>
    {


        public async Task<CqsResult<EntitySgiDtos>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
        {

            #region VALIDATION

            if (request == null)
                return CqsResult<EntitySgiDtos>.Failure(Error.Validation("Request cannot be null"));

            if(request.EntityId == null || request.EntityId == Guid.Empty) 
                return CqsResult<EntitySgiDtos>.Failure(Error.Validation("Id not valid or empty"));

            #endregion

            logger.LogInformation("Application : Request : EntityById =>");

            try
            {
                using var conn = await dapperConnection.CreateConnectionAsync();
                using IDbTransaction tx = conn.BeginTransaction();

               CqsResult<EntitySgiDtos> result = await entityService.GetEntityByIdQuery(request.EntityId!.Value, conn, tx);

                if (!result.IsSuccess)
                {
                    tx.Rollback();
                    return result;
                }

                tx.Commit();

                return result;

            }
            catch (SqlException ex)
            {
                var error = sqlExceptionTranslator.Translate(ex);
                logger.LogError(ex, "SQL Exception occurred while creating entity");
                return CqsResult<EntitySgiDtos>.Failure(error);
            }



        }
    }
}
