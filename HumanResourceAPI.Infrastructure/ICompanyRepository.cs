using Entities.Models;

namespace HumanResourceAPI.Infrastructure
{
    public interface ICompanyRepository : IRepositoryBase<Company, Guid>
    {
    }
}
