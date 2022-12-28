using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResourceAPI.Controllers
{
    //[ApiVersion("1.0")]
    [Route("api/companies")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
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
        [Authorize]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParameters companyParameters)
        {
            try
            {
                var companies = await _repository.Company.GetCompaniesAsync(companyParameters, trackChange: false);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(companies.MetaData));

                var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

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
                _logger.LogError($"{nameof(CompanyCreationDto)} object sent from client is null");
                return BadRequest($"{nameof(CompanyCreationDto)} is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the {nameof(CompanyCreationDto)} object");
                return UnprocessableEntity(ModelState);
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

        [HttpPut(template:"{id}", Name = RouteNames.UpdateCompany)]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdatingDto company)
        {
            if (company == null)
            {
                _logger.LogError($"{nameof(CompanyUpdatingDto)} object sent from client is null");
                return BadRequest($"{nameof(CompanyUpdatingDto)} is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the {nameof(CompanyUpdatingDto)} object");
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

        [HttpPatch(template:"{id}")]
        public async Task<IActionResult> PartiallyUpdateCompany(Guid id, [FromBody] JsonPatchDocument<CompanyUpdatingDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError($"{nameof(patchDoc)} object sent form client is null");
                return BadRequest($"{nameof(patchDoc)} is null");
            }

            var companyEntity = await _repository.Company.FindByIdAsync(id);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id {id} doesn't exist in the database.");
                return NotFound();
            }

            var companyToPatch = _mapper.Map<CompanyUpdatingDto>(companyEntity);

            TryValidateModel(companyToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the {nameof(patchDoc)} document");
                return UnprocessableEntity(ModelState);
            }

            patchDoc.ApplyTo(companyToPatch);

            _mapper.Map(companyToPatch, companyEntity);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
