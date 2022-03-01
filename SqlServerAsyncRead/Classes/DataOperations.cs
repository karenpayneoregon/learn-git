using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SqlServerAsyncRead.Interfaces;

namespace SqlServerAsyncRead.Classes
{
    public class DataOperations : IBaseOperations
    {
        private static string _connectionString = "";
        public static bool RunWithoutIssues = false;

        public static async Task<DataTableResults> ReadTask()
        {
            var result = new DataTableResults() { DataTable = new DataTable() };

            _connectionString = RunWithoutIssues ?
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NorthWind2020;Integrated Security=True" :
                "Data Source=(localdb)\\MSSQLLocalDB1;Initial Catalog=NorthWind2020;Integrated Security=True";

            return await Task.Run(async () =>
            {

                await using var cn = new SqlConnection( _connectionString );
                await using var cmd = new SqlCommand() { Connection = cn };

                cmd.CommandText = SelectStatement();

                try
                {
                    await cn.OpenAsync();
                }
                catch (Exception ex)
                {
                    Exceptions.Write(ex, ExceptionLogType.General);
                    result.GeneralException = ex;
                    return result;
                }

                result.DataTable.Load(await cmd.ExecuteReaderAsync());

                return result;

            });

        }

        /// <summary>
        /// SELECT Products
        /// </summary>
        /// <returns></returns>
        private static string SelectStatement() =>
        @"SELECT P.ProductID, 
               P.ProductName, 
               P.SupplierID, 
               S.CompanyName, 
               C.CategoryName, 
               P.QuantityPerUnit, 
               P.UnitPrice, 
               P.UnitsInStock, 
               P.UnitsOnOrder
        FROM Products AS P
             INNER JOIN Categories AS C ON P.CategoryID = C.CategoryID
             INNER JOIN Suppliers AS S ON P.SupplierID = S.SupplierID
        WHERE P.CategoryID = 3
              AND P.UnitsOnOrder > 0
        ORDER BY P.ProductName;";
    }
}
