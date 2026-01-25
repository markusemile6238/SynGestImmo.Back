using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Infrastructure.Data
{
    public interface IDapperConnection
    {
            Task<IDbConnection> CreateConnectionAsync();
    }
}
