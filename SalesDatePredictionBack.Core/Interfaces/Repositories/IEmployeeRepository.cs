using SalesDatePredictionBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployees();
    }
}
