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
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object);
        }

        [Fact]
        public async Task AddOrderWithProductsAsync_CallsRepositoryMethod()
        {
            // Arrange
            var orderDTO = new OrderDTO
            {
                EmpId = 1,
                ShipperId = 1,
                ShipName = "Test Ship",
                ShipAddress = "Test Address",
                ShipCity = "Test City",
                OrderDate = System.DateTime.Now,
                RequiredDate = System.DateTime.Now.AddDays(1),
                ShippedDate = System.DateTime.Now.AddDays(2),
                Freight = 10,
                ShipCountry = "Test Country",
                CustId = 1,
                OrderDetail = new OrderDetail { ProductId = 1, OrderId = 1, UnitPrice = 10, Qty = 1, Discount = 0 }
            };

            // Act
            await _orderService.AddOrderWithProductsAsync(orderDTO);

            // Assert
            _mockOrderRepository.Verify(repo => repo.AddOrderWithProductsAsync(It.IsAny<Order>(), It.IsAny<OrderDetail>()), Times.Once);
        }

        [Fact]
        public void GetOrders_ReturnsOrders()
        {
            // Arrange
            var custId = 1;
            var orders = new List<OrderToGet>
            {
                new OrderToGet { OrderId = 1, ShipName = "Test Ship", RequiredDate = DateTime.Now, ShippedDate = DateTime.Now.AddDays(1), ShipAddress = "Test Address", ShipCity = "Medellín" }
            };
            _mockOrderRepository.Setup(repo => repo.GetOrders(custId)).Returns(orders);

            // Act
            var result = _orderService.GetOrders(custId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().OrderId);
        }

        [Fact]
        public void GetSaleDatePrediction_ReturnsPredictions()
        {
            // Arrange
            var custId = 1;
            var companyName = "Test Company";
            var predictions = new List<SaleDatePrediction>
            {
                new SaleDatePrediction {CustId = custId, CompanyName = companyName, LastOrderDate = DateTime.Now.AddDays(-5), NextPredictedOrder = DateTime.Now }
            };
            _mockOrderRepository.Setup(repo => repo.GetSaleDatePrediction(companyName)).Returns(predictions);

            // Act
            var result = _orderService.GetSaleDatePrediction(companyName);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(companyName, result.First().CompanyName);
        }
    }
}
