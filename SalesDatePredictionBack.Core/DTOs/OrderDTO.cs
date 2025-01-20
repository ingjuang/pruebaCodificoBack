using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Core.Entities
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? EmpId { get; set; }
        public int? ShipperId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public float? Freight { get; set; }
        public string? ShipCountry { get; set; }
        public int? CustId { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
