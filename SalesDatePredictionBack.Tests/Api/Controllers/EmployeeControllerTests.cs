using Microsoft.AspNetCore.Mvc;
using Moq;
using SalesDatePredictionBack.Api.Controllers;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Tests.Api.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object);
        }

        [Fact]
        public void GetEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Empid = 1, FullName = "John Doe" },
                new Employee { Empid = 2, FullName = "Jane Smith" }
            };
            _mockEmployeeService.Setup(service => service.GetEmployees()).Returns(employees);

            // Act
            var result = _employeeController.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Employee>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
