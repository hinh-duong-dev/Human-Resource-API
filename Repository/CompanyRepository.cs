using Entities;
using Entities.Models;
using HumanResourceAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) :base(context)
        {
        
        }
    }
}
