using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SGI.Service.Identity") ?? throw new ArgumentNullException("SGI.Service.Identity Connection string not found");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
