using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResourceAPI.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        {
            _repository= repository;
            _logger= logger;
            _mapper= mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] EmployeeCreationDto employee)
        { 
            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null) 
            {
                _logger.LogError($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);

            employeeEntity.CompanyId = companyId;

            _repository.Employee.Create(employeeEntity);
            await _repository.SaveChangesAsync();

            var employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);

            return Ok(employeeDto);
        }

        [HttpPost(template: "collection")]
        public async Task<IActionResult> CreateEmployeeCollection(Guid companyId, IEnumerable<EmployeeCreationDto> employeeCollection)
        {
            if (employeeCollection == null)
            {
                _logger.LogError("Employee collection sent from client is null.");
                return BadRequest("Employee collection is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null) 
            {
                _logger.LogError($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeEntities = _mapper.Map<IEnumerable<Employee>>(employeeCollection);

            foreach (var employee in employeeEntities)
            {
                employee.CompanyId = companyId;
                _repository.Employee.Create(employee);
            }

            await _repository.SaveChangesAsync();

            var employeeCollectionDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);

            return Ok(employeeCollectionDto);
        }

        [HttpGet(template: "{id}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid Id)
        {
            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null)
            {
                _logger.LogError($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeDb = await _repository.Employee.FindByIdAsync(Id);

            if (employeeDb == null)
            {
                _logger.LogError($"Employee with id: {Id} doesn't exist in the database");
                return NotFound();
            }

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, [FromBody]EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange)
            { 
               return BadRequest("Max age can't be less than min age");
            }

            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null) 
            {
                _logger.LogInfo($"Company with id {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeesFromDb = await _repository.Employee.GetEmployeeAsync(companyId, employeeParameters, trackChange: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesFromDb.MetaData));

            var employeesDto = _mapper.Map<IEnumerable<Employee>>(employeesFromDb);

            return Ok(employeesDto);
        }
    }
}
