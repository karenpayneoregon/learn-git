using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Targets;
using SqlServerAsyncRead.Interfaces;
using StopWatchLibrary;

namespace SqlServerAsyncRead.Classes
{
    public class DataOperations : ICommonService
    {
        private static string _connectionString = "";
        public static bool RunWithoutIssues = false;

        private readonly ILogger _logger;
        private readonly ServiceProvider _provider;
        public DataOperations()
        {
            
        }

        public DataOperations(ServiceProvider provider)
        {
            _provider = provider;
            _logger = provider.GetService<ILoggerFactory>().CreateLogger<DataOperations>();
        }

        public bool FoolishAttemptToConnect()
        {
            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB1;" + 
                                            "Initial Catalog=NorthWind2020;Integrated Security=True";
            StopWatcher.Instance.Start();
            try
            {
                using var cn = new SqlConnection(connectionString);
                cn.Open();
                return true;
            }
            catch 
            {
                StopWatcher.Instance.Stop();
                Debug.WriteLine(StopWatcher.Instance.ElapsedFormatted);
                return false;
            }
        }

        public async Task<(DataTable dt, bool)> ReadTask(CancellationToken ct)
        {
            DataTable dt = new DataTable();

            _connectionString = RunWithoutIssues ?
                // good
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NorthWind2020;Integrated Security=True" :
                // bad
                "Data Source=(localdb)\\MSSQLLocalDB1;Initial Catalog=NorthWind2020;Integrated Security=True";

            await using var cn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand() { Connection = cn };

            cmd.CommandText = SelectStatement();

            try
            {
                //Debug.WriteLine(_logger.IsEnabled(LogLevel.Information));
                await cn.OpenAsync(ct);
                dt.Load(await cmd.ExecuteReaderAsync(ct));
                EventId eventId = new(0, "Read");
                _logger.LogInformation(eventId, "Data loaded");
                return (dt, true);
            }
            catch (TaskCanceledException tce)
            {
                EventId eventId = new(1, "Unrecoverable");
                _logger.LogCritical(eventId, $"Connection string '{_connectionString}'");
                return (dt, false);
            }
            catch (Exception localException)
            {
                EventId eventId = new(10, "Exception thrown");
                _logger.LogCritical(eventId, localException.Message);
                return (null, false);
            }

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
