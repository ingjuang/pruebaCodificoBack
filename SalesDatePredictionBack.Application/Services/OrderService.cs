using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Repositories;
using SalesDatePredictionBack.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task AddOrderWithProductsAsync(OrderDTO orderDTO)
        {
            Order order = new Order
            {
                EmpId = orderDTO.EmpId,
                ShipperId = orderDTO.ShipperId,
                ShipName = orderDTO.ShipName,
                ShipAddress = orderDTO.ShipAddress,
                ShipCity = orderDTO.ShipCity,
                OrderDate = orderDTO.OrderDate,
                RequiredDate = orderDTO.RequiredDate,
                ShippedDate = orderDTO.ShippedDate,
                Freight = orderDTO.Freight,
                ShipCountry = orderDTO.ShipCountry,
                CustId = orderDTO.CustId,
            };
            await _orderRepository.AddOrderWithProductsAsync(order, orderDTO.OrderDetail);
        }

        public IEnumerable<OrderToGet> GetOrders(int custId)
        {
            return _orderRepository.GetOrders(custId);
        }

        public IEnumerable<SaleDatePrediction> GetSaleDatePrediction(string companyName = null)
        {
            return _orderRepository.GetSaleDatePrediction(companyName);
        }
    }
}
