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
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _orderController = new OrderController(_mockOrderService.Object);
        }

        [Fact]
        public void GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var custId = 1;
            var orders = new List<OrderToGet>
            {
                new OrderToGet { OrderId = 1, ShipName = "Test Ship", RequiredDate = DateTime.Now, ShippedDate = DateTime.Now.AddDays(1), ShipAddress = "Test Address", ShipCity = "Medellín" }
            };
            _mockOrderService.Setup(service => service.GetOrders(1)).Returns(orders);

            // Act
            var result = _orderController.GetOrders(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<OrderToGet>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetSaleDatePrediction_ReturnsOkResult_WithPrediction()
        {
            // Arrange
            var custId = 1;
            var companyName = "Test Company";
            var predictions = new List<SaleDatePrediction>
            {
                new SaleDatePrediction {CustId = custId, CompanyName = companyName, LastOrderDate = DateTime.Now.AddDays(-5), NextPredictedOrder = DateTime.Now }
            };
            _mockOrderService.Setup(service => service.GetSaleDatePrediction(companyName)).Returns(predictions);

            // Act
            var result = _orderController.GetSaleDatePrediction(companyName);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddOrderWithProducts_ReturnsOkResult()
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
            _mockOrderService.Setup(service => service.AddOrderWithProductsAsync(orderDTO)).Returns(Task.CompletedTask);

            // Act
            var result = await _orderController.AddOrderWithProducts(orderDTO);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
