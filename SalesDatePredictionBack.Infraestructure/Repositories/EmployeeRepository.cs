using Microsoft.EntityFrameworkCore;
using SalesDatePredictionBack.Core.Entities;
using SalesDatePredictionBack.Core.Interfaces.Repositories;
using SalesDatePredictionBack.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Infraestructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreSampleContext _context;

        public EmployeeRepository(StoreSampleContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var query = "SELECT Empid, (firstname + ' ' + lastname) AS FullName FROM StoreSample.HR.Employees";
            return _context.Employees.FromSqlRaw(query).ToList();
        }

    }
}
