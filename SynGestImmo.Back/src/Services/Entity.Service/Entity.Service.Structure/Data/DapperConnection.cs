using Entity.Service.Application.Common;
using Entity.Service.Domain.ExceptionService;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Entity.Service.Structure.Data
{
    public class DapperConnection : IDapperConnection
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<DapperConnection> _logger;

        public DapperConnection(IConfiguration configuration, ILogger<DapperConnection> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("SGI.Service.Entity") ?? throw new InvalidOperationException("Connection string 'SGI.Service.Entity' not found");

            if (String.IsNullOrWhiteSpace(_connectionString)) throw new InvalidOperationException("Connection string 'SGI.Service.Entity' is missing or empty in appsettings.json");

            _logger.LogInformation("Connection string configuration: {ConnectionState}", _connectionString);

        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
        try
        {

            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            _logger.LogInformation("Connection open");
             return connection;
        }
        catch (SqlException ex) when (ex.Number == 4060) // database not found  
        {
            _logger.LogError(ex,"Error sql connection : {Message}\n Sql Error Number: : {Number}",ex.Message,ex.Number);
            throw new EntityServiceExceptions("DATABASE_CONNECTION_FAILED", "cannot connect to database", 500);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex,"Database error : {Message}\n Sql Error Number:{Number}",ex.Message,ex.Number);
            throw new EntityServiceExceptions("DATABASE_ERROR", $"Database error :{ex.Message}", 500);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Unexpected error : {Message}\n <<<<<ERROR>>>>>{Ex}",ex.Message,ex);
            throw new EntityServiceExceptions("CONNECTION_UNEXPECTED_ERROR",
                "Unexpected connection error", 500);
        }

    }
    }
}
