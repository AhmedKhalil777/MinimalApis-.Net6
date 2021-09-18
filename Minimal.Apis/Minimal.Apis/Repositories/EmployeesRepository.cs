using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.Extensions.Logging;

namespace Minimal.Apis.Repositories
{
    public class EmployeesRepository
    {
        private readonly ILogger<EmployeesRepository> _logger;
        private readonly List<Employee> _employees = new();
        public EmployeesRepository(ILogger<EmployeesRepository> logger)
        {
            _logger = logger;
            var customerFaker = new Faker<Employee>()
                .RuleFor(employee => employee.Id, faker => Guid.NewGuid())
                .RuleFor(employee => employee.DateOfBirth, faker => faker.Person.DateOfBirth)
                .RuleFor(employee => employee.Department, faker => faker.Commerce.Department())
                .RuleFor(employee => employee.UserName, faker => faker.Person.FullName)
                .RuleFor(employee => employee.Salary, faker => faker.Random.Double(5000D, 50000D));
            _employees.AddRange(customerFaker.GenerateLazy(100));
        }

        public IEnumerable<Employee> GetEmployees() => _employees;
        public Employee? GetEmployee(Guid empId) => _employees.FirstOrDefault(x => x.Id == empId);

        public void UpdateEmployee(Employee employee)
        {
            var emp = _employees.FirstOrDefault(emp => emp.Id == employee.Id);
            if (emp is null) return;
            emp.Department = employee.Department;
            emp.DateOfBirth = employee.DateOfBirth;
            emp.UserName = employee.UserName;
            emp.Salary = employee.Salary;
        }

        public void AddEmployee(Employee employee)
        {
            _employees.Add(employee);
        }

        public void DeleteEmployee(Guid employeeId)
        {
            var emp = _employees.FirstOrDefault(emp => emp.Id == employeeId);
            if (emp is null) return;
            _employees.Remove(emp);
        }
    }
}