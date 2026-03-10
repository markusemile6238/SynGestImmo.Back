using Tools.Result;
using Microsoft.Data.SqlClient; 

namespace Entity.Service.Application.Common
{
    public interface ISqlExceptionTranslator
    {
        CqsError Translate(SqlException ex);
    }
}
