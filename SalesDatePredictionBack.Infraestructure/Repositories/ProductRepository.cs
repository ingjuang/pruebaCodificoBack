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
    public class ProductRepository : IProductRepository
    {
        private readonly StoreSampleContext _context;

        public ProductRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            var query = "SELECT productid, productname FROM StoreSample.Production.Products";
            return _context.Products.FromSqlRaw(query).ToList();
        }
    }
}
