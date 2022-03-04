using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BaseCoreUnitTestProject1.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseCoreUnitTestProject1
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public partial class DateTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.Nullable)]
        [Ignore]
        public void NonNullableBirthYear()
        {
            var current = _peopleList.FirstOrDefault(person => person.FirstName == "Karen");
            Assert.AreEqual(current!.BirthYear, 1956);
        }

        [TestMethod]
        [TestTraits(Trait.Dates)]
        public void NullableHireDate()
        {
            var current = _peopleList.FirstOrDefault(person => person.FirstName == "Karen");
            Assert.IsTrue(current.HireDate.HasValue);
        }

    }

}