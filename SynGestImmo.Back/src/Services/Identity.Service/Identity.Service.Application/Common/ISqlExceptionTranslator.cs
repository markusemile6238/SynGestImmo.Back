using Microsoft.Data.SqlClient;
using Tools.Result;

namespace Identity.Service.Application.Common
{
    public interface ISqlExceptionTranslator
    {
        CqsError Translate(SqlException exception);
    }
}
