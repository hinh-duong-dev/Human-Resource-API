using Entities.Models;
using Entities.RequestFeatures;

namespace HumanResourceAPI.Infrastructure
{
    public interface ICompanyRepository : IRepositoryBase<Company, Guid>
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChange);
    }
}
