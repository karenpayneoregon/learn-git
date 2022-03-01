using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlServerAsyncRead.Classes;

namespace SqlServerAsyncRead.Interfaces
{
    public interface IBaseOperations
    {
        //Task<DataTableResults> ReadTask();
        static string GetHello() => throw new NotImplementedException();
        public static string Something = "something";
    }
}
