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
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod1()
        {

            var current = Mocked.People.FirstOrDefault(person => person.FirstName == "Karen");
            if (current is not null)
            {
                Assert.AreEqual(current.YearOfBirth, 1956);
            }

        }

        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void TestMethod2()
        {
            // TODO
        }
    }
}
