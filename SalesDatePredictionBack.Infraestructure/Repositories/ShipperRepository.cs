using Microsoft.EntityFrameworkCore;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Repositories;
using SalesDatePredictionBack.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Infraestructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly StoreSampleContext _context;

        public ShipperRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public IEnumerable<Shipper> GetShippers()
        {
            var query = "SELECT shipperid, companyname FROM StoreSample.Sales.Shippers";
            return _context.Shippers.FromSqlRaw(query).ToList();

        }
    }
}
