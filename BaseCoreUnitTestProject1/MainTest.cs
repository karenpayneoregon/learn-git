using System;
using System.Linq;
using BaseCoreUnitTestProject1.Base;
using BaseNetCoreClassProject1.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseCoreUnitTestProject1
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
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
        [TestTraits(Trait.PlaceHolder)]
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
    }
}
