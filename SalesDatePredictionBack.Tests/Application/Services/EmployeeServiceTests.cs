using Moq;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Repositories;
using SalesDatePredictionBack.Application.Services;
using System.Collections.Generic;
using Xunit;

namespace SalesDatePredictionBack.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object);
        }

        [Fact]
        public void GetEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Empid = 1, FullName = "John Doe" },
                new Employee { Empid = 2, FullName = "John Maicol" }
            };
            _mockEmployeeRepository.Setup(repo => repo.GetEmployees()).Returns(employees);

            // Act
            var result = _employeeService.GetEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("John Doe", result.First().FullName);
        }
    }
}