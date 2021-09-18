using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.Extensions.Logging;

namespace Minimal.Apis.Repositories
{
    public class CustomersRepository
    {
        private readonly ILogger<CustomersRepository> _logger;
        private List<Employee> _customers;
        public CustomersRepository(ILogger<CustomersRepository> logger)
        {
            _logger = logger;
            var customerFaker = new Faker<Employee>()
                .RuleFor(customer => customer.Id, faker => Guid.NewGuid())
                .RuleFor(customer => customer.DateOfBirth, faker =>  faker.Person.DateOfBirth)
                .RuleFor(customer => customer.Department, faker =>  faker.Commerce.Department())
                .RuleFor(customer => customer.Department, faker =>  faker.Commerce.Department())

        }
    }
}