using SalesDatePredictionBack.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePredictionBack.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        public IEnumerable<Employee> GetEmployees();
    }
}
