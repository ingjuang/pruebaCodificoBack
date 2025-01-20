using Moq;
using SalesDatePredictionBack.Application.Services;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Tests.Application.Services
{
    public class ShipperServiceTests
    {
        private readonly Mock<IShipperRepository> _mockShipperRepository;
        private readonly ShipperService _shipperService;

        public ShipperServiceTests()
        {
            _mockShipperRepository = new Mock<IShipperRepository>();
            _shipperService = new ShipperService(_mockShipperRepository.Object);
        }

        [Fact]
        public void GetShippers_ReturnsListOfShippers()
        {
            // Arrange
            var shippers = new List<Shipper>
            {
                new Shipper { ShipperId = 1, CompanyName = "Shipper 1" },
                new Shipper { ShipperId = 2, CompanyName = "Shipper 2" }
            };
            _mockShipperRepository.Setup(repo => repo.GetShippers()).Returns(shippers);

            // Act
            var result = _shipperService.GetShippers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Shipper 1", result.First().CompanyName);
        }
    }
}
