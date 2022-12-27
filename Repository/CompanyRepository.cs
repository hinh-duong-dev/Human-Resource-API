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

        public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChange)
        {
            return await FindAll(trackChange)
                .OrderBy(c => c.Name)
                .Skip((companyParameters.PageNumber - 1) * companyParameters.PageSize)
                .Take(companyParameters.PageSize)
                .ToListAsync();
        }
    }
}
