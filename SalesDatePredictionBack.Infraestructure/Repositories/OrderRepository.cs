using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Repositories;
using SalesDatePredictionBack.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesDatePredictionBack.Infraestructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreSampleContext _context;

        public OrderRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderToGet> GetOrders(int custId)
        {
            var query = "";
            if (custId == 0)
            {
                query = "SELECT orderid, requireddate, shippeddate, shipname, shipaddress, shipcity FROM StoreSample.Sales.Orders";
            }
            else
            {
                query = $"SELECT orderid, requireddate, shippeddate, shipname, shipaddress, shipcity FROM StoreSample.Sales.Orders WHERE custid = {custId}";
            }
            return _context.ordersToGet.FromSqlRaw(query).ToList();
        }
        public IEnumerable<SaleDatePrediction> GetSaleDatePrediction(string companyName = null)
        {
            var query = "";
            if (string.IsNullOrEmpty(companyName))
            {
                query = "SELECT c.custid, c.companyname AS CompanyName, MAX(o.orderdate) AS LastOrderDate, CAST(DATEADD(SECOND, AVG(CAST(DATEDIFF(SECOND, '2000-01-01', o.orderdate) AS BIGINT)), '2000-01-01') AS DATETIME) AS NextPredictedOrder FROM StoreSample.Sales.Orders AS o INNER JOIN StoreSample.Sales.Customers AS c ON o.custid = c.custid GROUP BY c.custid, c.companyname;";
            }
            else
            {
                query = $"SELECT c.custid, c.companyname AS CompanyName, MAX(o.orderdate) AS LastOrderDate, CAST(DATEADD(SECOND, AVG(CAST(DATEDIFF(SECOND, '2000-01-01', o.orderdate) AS BIGINT)), '2000-01-01') AS DATETIME) AS NextPredictedOrder FROM StoreSample.Sales.Orders AS o INNER JOIN StoreSample.Sales.Customers AS c ON o.custid = c.custid WHERE c.companyname LIKE '%{companyName}%' GROUP BY c.custid, c.companyname;";
            }
            return _context.SaleDatesPrediction.FromSqlRaw(query).ToList();
        }

        public async Task AddOrderWithProductsAsync(Order order, OrderDetail orderDetail)
        {
            var insertOrderSql = @"
            INSERT INTO StoreSample.Sales.Orders 
            (Empid, Shipperid, Shipname, Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry, Custid)
            VALUES (@EmpId, @ShipperId, @ShipName, @ShipAddress, @ShipCity, @OrderDate, @RequiredDate, @ShippedDate, @Freight, @ShipCountry, @CustId);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = insertOrderSql;
                command.CommandType = System.Data.CommandType.Text;

                command.Parameters.Add(new SqlParameter("@EmpId", order.EmpId));
                command.Parameters.Add(new SqlParameter("@ShipperId", order.ShipperId));
                command.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                command.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                command.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                command.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                command.Parameters.Add(new SqlParameter("@RequiredDate", order.RequiredDate));
                command.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                command.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                command.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                command.Parameters.Add(new SqlParameter("@CustId", order.CustId));

                await _context.Database.OpenConnectionAsync();
                var result = await command.ExecuteScalarAsync();
                await _context.Database.CloseConnectionAsync();

                order.OrderId = Convert.ToInt32(result);
            }

            var insertOrderDetailSql = @"
            INSERT INTO StoreSample.Sales.OrderDetails 
            (OrderId, ProductId, Qty, UnitPrice, Discount)
            VALUES (@OrderId, @ProductId, @Qty, @UnitPrice, @Discount);";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = insertOrderDetailSql;
                command.CommandType = System.Data.CommandType.Text;

                command.Parameters.Add(new SqlParameter("@OrderId", order.OrderId));
                command.Parameters.Add(new SqlParameter("@ProductId", orderDetail.ProductId));
                command.Parameters.Add(new SqlParameter("@Qty", orderDetail.Qty));
                command.Parameters.Add(new SqlParameter("@UnitPrice", orderDetail.UnitPrice));
                command.Parameters.Add(new SqlParameter("@Discount", orderDetail.Discount));

                await _context.Database.OpenConnectionAsync();
                await command.ExecuteNonQueryAsync();
                await _context.Database.CloseConnectionAsync();
            }

        }
    }
}
