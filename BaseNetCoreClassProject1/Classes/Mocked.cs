using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetCoreClassProject1.Classes
{
    public class Mocked
    {
        /// <summary>
        /// Mocked list of <see cref="Person"/>
        /// </summary>
        public static List<Person> People => new()
        {
            new() { Identifier = 1, FirstName = "Karen", LastName = "Payne", PostalCode = "A", BirthYear = 1956, HireDate = new DateTime(1999,12,23)},
            new() { Identifier = 2, FirstName = "Greg", LastName = "White", PostalCode = "B", BirthYear = 1967},
            new() { Identifier = 3, FirstName = "Jon", LastName = "Wingett", PostalCode = "C", BirthYear = 1960},
        };

        public static void Demo()
        {
            IEnumerable<Person> query = from p in People select p;
            Worker.CompareValue(People);

        }
    }
}
