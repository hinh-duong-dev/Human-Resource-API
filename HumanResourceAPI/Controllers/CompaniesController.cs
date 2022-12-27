using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        private static class RouteNames
        {
            public const string GetCompanies = nameof(GetCompanies);
            public const string GetCompanyById = nameof(GetCompanyById);
            public const string CreateCompany = nameof(CreateCompany);
            public const string DeleteCompany = nameof(DeleteCompany);
            public const string UpdateCompany = nameof(UpdateCompany);
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.FindAll(trackChange: false).ToList();
                var companyDtos = _mapper.Map<List<CompanyDto>>(companies);

                return Ok(companyDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at: {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }                    
        }

        [HttpGet(template:"{id}", Name = RouteNames.GetCompanyById)]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(id);
            if (company == null)
            {
                _logger.LogInfo($"Company with id {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto company)
        {
            if (company == null)
            {
                _logger.LogError($"CompanyCreationDto object sent from client is null");
                return BadRequest("CompanyCreationDto is null");
            }

            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.Create(companyEntity);

            await _repository.SaveChangesAsync();

            var companyDto = _mapper.Map<CompanyDto>(companyEntity);

            return Ok(companyDto);
        }

        [HttpDelete(template: "{id}", Name = RouteNames.DeleteCompany)]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(id);
            if (company == null)
            {
                _logger.LogInfo($"Company with id {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Company.Delete(company);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut(template: "{id}", Name = RouteNames.UpdateCompany)]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdatingDto company)
        {
            if (company == null)
            {
                _logger.LogError($"CompanyUpdatingDto object sent from client is null");
                return BadRequest("CompanyUpdatingDto is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CompanyUpdatingDto object");
                return UnprocessableEntity(ModelState);
            }

            var companyEntity = await _repository.Company.FindByIdAsync(id);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id {id} doesn't exist in the database");
                return NotFound();
            }

            _mapper.Map(company, companyEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
