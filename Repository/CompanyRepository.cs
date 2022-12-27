using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using HumanResourceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
 

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChange)
        {
            var companies = await FindAll(trackChange)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return PagedList<Company>.ToPagedList(companies, companyParameters.PageNumber, companyParameters.PageSize);
        }
    }
}
