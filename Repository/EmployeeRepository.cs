using Entities.Models;
using Entities;
using HumanResourceAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }
    }
}
