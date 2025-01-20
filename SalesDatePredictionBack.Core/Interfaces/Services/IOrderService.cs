using SalesDatePredictionBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Core.Interfaces.Services
{
    public interface IOrderService
    {
        public IEnumerable<OrderToGet> GetOrders(int custId);
        public IEnumerable<SaleDatePrediction> GetSaleDatePrediction(string companyName = null);
        public Task AddOrderWithProductsAsync(OrderDTO orderDTO);
    }
}
