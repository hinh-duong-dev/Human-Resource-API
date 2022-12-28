using AutoMapper;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceAPI.Controllers
{
    //[ApiVersion("2.0")]
    //[Route("api/companies")]
    [Route(template: "api/{v:apiversion}/companies")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompaniesV2Controller(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies() 
        {
            var companies = await _repository.Company.FindAll(trackChange: false).ToListAsync();

            return Ok(companies);
        }
    }
}
