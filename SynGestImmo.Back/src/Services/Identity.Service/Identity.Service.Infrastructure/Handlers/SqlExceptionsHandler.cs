using Identity.Service.Application.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data.SqlTypes;
using Tools.Result;

namespace Identity.Service.Infrastructure.Handlers
{
    public class SqlExceptionsHandler : ISqlExceptionTranslator
    {


        private readonly ILogger<SqlExceptionsHandler> _logger;

        public SqlExceptionsHandler(ILogger<SqlExceptionsHandler> logger)
        {
            _logger = logger;
        }


        public CqsError Translate(SqlException ex)
        {
            _logger.LogError($"Erreur SQL: {ex.Number} \n {ex.Message}");


            switch (ex.Number)
            {
                case 2627:
                    return Error.Validation("Duplicate Key Error");

                case 2601:
                    return Error.Validation("A violation of the uniqueness constraint has occurred.");

                case 547:
                    return Error.Validation("A foreign key constraint violation has occurred.");

                case 515:
                    return Error.Validation("Some required fields are missing.");

                case 8152:
                    return Error.Validation("Some fields exceed the maximum length");

                case 4060:
                    return Error.Database("Unable to connect to the database");

                case 18456:
                    return Error.Database("Database authentication failed");

                default:
                    return Error.Database("Database error");
            }
        }

    }
}

