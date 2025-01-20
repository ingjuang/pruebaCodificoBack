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
    public class ShipperControllerTests
    {
        private readonly Mock<IShipperService> _mockShipperService;
        private readonly ShipperController _shipperController;

        public ShipperControllerTests()
        {
            _mockShipperService = new Mock<IShipperService>();
            _shipperController = new ShipperController(_mockShipperService.Object);
        }

        [Fact]
        public void GetShippers_ReturnsOkResult_WithListOfShippers()
        {
            // Arrange
            var shippers = new List<Shipper>
            {
                new Shipper { ShipperId = 1, CompanyName = "Shipper 1" },
                new Shipper { ShipperId = 2, CompanyName = "Shipper 2" }
            };
            _mockShipperService.Setup(service => service.GetShippers()).Returns(shippers);

            // Act
            var result = _shipperController.GetShippers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Shipper>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}
