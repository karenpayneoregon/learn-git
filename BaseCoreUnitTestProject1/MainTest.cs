using System;
using System.Linq;
using System.Threading.Tasks;
using BaseCoreUnitTestProject1.Base;
using BaseNetCoreClassProject1.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlServerAsyncRead.Classes;

namespace BaseCoreUnitTestProject1
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.Nullable)]
        public void TestMethod1()
        {

            var current = Mocked.People.FirstOrDefault(person => person.FirstName == "Karen");
            var year = current!.BirthYear;
            if (year != null)
            {
                Assert.AreEqual(year, 1956);
            }

        }

        [TestMethod]
        [TestTraits(Trait.Dates)]
        public void TestMethod2()
        {
            var current = Mocked.People.FirstOrDefault(person => person.FirstName == "Karen");
            if (current is not null)
            {
                if (current.HireDate.HasValue)
                {
                    Console.WriteLine(current.HireDate.Value);
                }
            }
        }

        [TestMethod]
        [TestTraits(Trait.NorthWind)]
        public async Task NorthReadProducts_Good()
        {
            DataOperations.RunWithoutIssues = true;
            DataTableResults results = await DataOperations.ReadTask();
            Assert.AreEqual(results.HasException, false);
        }
        [TestMethod]
        [TestTraits(Trait.NorthWind)]
        public async Task NorthReadProducts_Bad()
        {
            DataOperations.RunWithoutIssues = false;
            DataTableResults results = await DataOperations.ReadTask();
            Assert.AreEqual(results.HasException, true);
        }
    }
}
