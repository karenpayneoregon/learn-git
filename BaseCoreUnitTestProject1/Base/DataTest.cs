using System;
using System.Collections.Generic;
using System.Threading;
using BaseCoreUnitTestProject1.Base;
using BaseNetCoreClassProject1.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Extensions.Logging;
using SqlServerAsyncRead.Classes;
using SqlServerAsyncRead.Interfaces;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;



// ReSharper disable once CheckNamespace
namespace BaseCoreUnitTestProject1
{
    public partial class DataTest
    {
        /// <summary>
        /// See notes in <see cref="Initialization"/> for usage
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
        
        /// <summary>
        /// Used to reduce load time in test methods
        /// </summary>
        private static List<Person> _peopleList;

        private DataOperations _dataOperations;

        private ServiceProvider serviceProvider;
        private ILogger<DataOperations> _logger;

        private void LogConfiguration()
        {

            serviceProvider = new ServiceCollection()
                .AddSingleton<ICommonService, DataOperations>()
                .AddLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .BuildServiceProvider();


            _logger = serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<DataOperations>();

            // disable all logging
            //if (true)
            //{
            //    LogManager.DisableLogging();
            //}
            

            // how to dynamically change a log file name by, in this case environment
            //var target = (FileTarget)LogManager.Configuration.FindTargetByName("development");
            //target.FileName = "DevLog.txt";
            //LogManager.ReconfigExistingLoggers();

            //LogManager.Configuration.Variables["myLevel"] = "Debug";
            //LogManager.ReconfigExistingLoggers();
            
        }


        /// <summary>
        /// Perform initialization before test runs using assertion on current test name.
        /// </summary>
        [TestInitialize]
        public void Initialization()
        {

            if (TestContext.TestName is nameof(NorthReadProducts_Good) or nameof(NorthReadProducts_Bad) or nameof(NorthFoolish))
            {
                /*
                 * Setup the CancellationTokenSource to time-out in four seconds, else when encountering
                 * a bad connection to a database server the default time-out is over 20 seconds.
                 *
                 * For a real application the time-out would be more like 1 or 2 seconds
                 */
                _cancellationTokenSource = new(TimeSpan.FromSeconds(1));


                LogConfiguration();

                _logger.LogInformation($"Starting {TestContext.TestName}");
                _dataOperations = new DataOperations(serviceProvider);

            }
        }
        
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestBase.TestResults = new List<TestContext>();
            _peopleList = Mocked.People;
        }
    }
}
