using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Result
{
    public interface IServiceException 
    {
        int StatusCode { get; }
        string? ErrorCode { get; }
    }
}
