using Entities;
using HumanResourceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private AppDbContext _dbContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public RepositoryManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICompanyRepository Company
        {
            get 
            {
                if (_companyRepository == null)
                {
                    _companyRepository = new CompanyRepository(_dbContext);
                }

                return _companyRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_companyRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_dbContext);
                }

                return _employeeRepository;
            }
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
