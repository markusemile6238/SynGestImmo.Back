using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Common
{
    public interface ISqlExceptionTranslator
    {
        CqsError Translate(SqlException exception);
    }
}
