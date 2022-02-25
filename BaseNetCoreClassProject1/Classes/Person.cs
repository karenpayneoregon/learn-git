using System;
using BaseNetCoreClassProject1.Interfaces;
using Microsoft.VisualBasic;

namespace BaseNetCoreClassProject1.Classes
{
    public class Person : IBase
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Identifier { get; set; }
        public int Id => Identifier;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
        public int? BirthYear { get; set; }
        public DateTime? HireDate { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public override string ToString() => $"{Id,-5}{FirstName} {LastName}";
    }
}
