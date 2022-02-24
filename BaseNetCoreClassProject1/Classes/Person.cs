using BaseNetCoreClassProject1.Interfaces;

namespace BaseNetCoreClassProject1.Classes
{
    public class Person : IBase
    {
        public int Identifier { get; set; }
        public int Id => Identifier;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
        public int YearOfBirth { get; set; }
        public override string ToString() => $"{Id,-5}{FirstName} {LastName}";
    }
}
