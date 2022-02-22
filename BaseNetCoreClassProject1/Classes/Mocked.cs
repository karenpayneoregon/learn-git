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
            new() { Identifier = 1, First_Name = "Karen", Last_Name = "Payne"},
            new() { Identifier = 2, First_Name = "Greg", Last_Name = "White"},
            new() { Identifier = 3, First_Name = "Jon", Last_Name = "Wingett"},
        };

        public static void Demo()
        {
            IEnumerable<Person> query = from p in People select p;
            Worker.CompareValue(People);

        }
    }
}
