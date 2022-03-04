using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BaseCoreUnitTestProject1.Base;
using BaseNetCoreClassProject1.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServerAsyncRead.Classes;

namespace BaseCoreUnitTestProject1
{
    [TestClass]
    public partial class DataTest : TestBase
    {


        [TestMethod]
        [TestTraits(Trait.NorthWind)]
        public async Task NorthReadProducts_Good()
        {
            DataOperations.RunWithoutIssues = true;
            var ( _ , success) = await _dataOperations.ReadTask(_cancellationTokenSource.Token);
            Assert.AreEqual(success, true);
            Debug.WriteLine(success);
        }

        [TestMethod]
        [TestTraits(Trait.NorthWind)]
        public async Task NorthReadProducts_Bad()
        {
            DataOperations.RunWithoutIssues = false;
            var ( _ , success) = await _dataOperations.ReadTask(_cancellationTokenSource.Token);

            Assert.AreEqual(success, false);

        }

        [TestMethod]
        [TestTraits(Trait.NorthWind)]
        [Ignore]
        public void NorthFoolish()
        {
            bool result = _dataOperations.FoolishAttemptToConnect();
            Assert.AreEqual(result, false);
        }

    }
}
