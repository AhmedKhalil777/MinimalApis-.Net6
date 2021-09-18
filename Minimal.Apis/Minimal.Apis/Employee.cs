using System;

namespace Minimal.Apis
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
    }
}