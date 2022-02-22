using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseNetCoreClassProject1.Classes
{
    public class Mocked
    {
        public static List<Person> People => new()
        {
            new() { Identifier = 1, FirstName = "Karen", LastName = "Payne"},
            new() { Identifier = 2, FirstName = "Greg", LastName = "White"},
            new() { Identifier = 3, FirstName = "Jon", LastName = "Wingett"},
        };

        public static void Demo()
        {
            IEnumerable<Person> query = from p in People select p;
            Worker.CompareValue(People);

        }
    }
}
