using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SqlServerAsyncRead.Interfaces
{
    public interface ICommonService
    {
        async Task<(DataTable dt, bool)> ReadTask(CancellationToken ct) 
            => throw new NotImplementedException();
    }
}