using Identity.Service.Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Infrastructure.Data
{
    public class DapperContext : IDapperConnection
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<DapperContext> _logger;

        public DapperContext(IConfiguration configuration, ILogger<DapperContext> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("SGI.Service.Identity") ?? throw new InvalidOperationException("Connection string 'SGI.Service.Identity' not found");

            if (String.IsNullOrWhiteSpace(_connectionString)) throw new InvalidOperationException("Connection string 'SGI.Service.Identity' is missing or empty in appsettings.json");

            _logger.LogInformation($"Connection string configuration {_connectionString}");

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
                _logger.LogError($"Error sql connection : {ex.Message}\n Sql Error Number: : {ex.Number}");
                throw new IdentityServiceException("DATABASE_CONNECTION_FAILED", "cannot connect to database",500);
            }
            catch (SqlException ex) 
            {
                _logger.LogError($"Database error : {ex.Message}\n Sql Error Number:{ex.Number}");
                throw new IdentityServiceException("DATABASE_ERROR", $"Database error :{ex.Message}", 500);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unexpected error : {ex.Message}\n <<<<<ERROR>>>>>{ex}");
                throw new IdentityServiceException("CONNECTION_UNEXPECTED_ERROR",
                    "Unexpected connection error", 500);
            }
        }

    }
}
