using Entities.Models;
using Entities;
using HumanResourceAPI.Infrastructure;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {         
        }

        public async Task<PagedList<Employee>> GetEmployeeAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange)
        {
            var employees = await FindAll(trackChange, e => e.Company.Equals(companyId))
                    .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                    .Search(employeeParameters.SearchTerm)
                    .OrderBy(e => e.FirstName)
                    .ToListAsync();

            return PagedList<Employee>
                .ToPagedList(employees, employeeParameters.PageNumber,
                employeeParameters.PageSize);
        }
    }
}
