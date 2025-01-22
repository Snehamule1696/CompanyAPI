using CompanyAPI.Data;
using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;
using CompanyAPI.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using System.Linq;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDbContext companyDbContext;
        private readonly ICompanyRepository companyRepository;
        private readonly IDepartmentRepository departmentRepository;

        public CompanyController(CompanyDbContext companyDbContext,ICompanyRepository companyRepository, IDepartmentRepository departmentRepository)
        {
            this.companyDbContext = companyDbContext;
            this.companyRepository = companyRepository;
            this.departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<CompanyDTO>>> GetAllCompanyDetails()
        {

            var companyData = await companyRepository.GetCompanyDetailsAsync(); 

            
            return companyData;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompanyById(int id)
        {
            var recordDto = await companyRepository.GetCompanyByIDAsync(id);

            if (recordDto == null)
            {
                return NotFound();
            }

            return Ok(recordDto); 
        }
        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> CreateCompanyAsync(Company company)
        {
            //Model Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check data alredy exist

            var createdRecord = await companyRepository.GetCompanyByNameAsync(company.Name);

            if(createdRecord != null)
                {
                return BadRequest($"{company.Name} is already exist");
                }
            //check length of company
            if (company.Name.Length < 3 || company.Name.Length > 100)
            {
                return BadRequest("Company name must be between 3 and 100 characters.");
            }

            //Validate name format (only letters and spaces)
            if (!Regex.IsMatch(company.Name, @"^[a-zA-Z\s]+$"))
            {
                return BadRequest("Company name can only contain letters and spaces.");
            }
            //Company location validation
            if (string.IsNullOrWhiteSpace(company.Location) || company.Location.Length < 4)
            {
                return BadRequest("Company address must be at least 5 characters long.");
            }

            if (!Regex.IsMatch(company.Location, @"^[a-zA-Z0-9\s,.-]+$"))
            {
                return BadRequest("Company address contains invalid characters.");
            }
            
            await companyRepository.CreateCompanyAsync(company);
            var createdRecordDto = new CompanyDTO
            {
                Id = createdRecord.Id,
                Name = createdRecord.Name,
                Location = createdRecord.Location,
            };

            return createdRecordDto;
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyDTO>> UpdateCompanyAsync(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRecord = await companyRepository.UpdateCompanyAsync(id, company);

            if (updatedRecord == null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }

  
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompanyAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departments = await departmentRepository.GetByCompanyIdAsync(id);
            if (departments != null)
            {
                return BadRequest("This company cannot be deleted because it is associated with one or more departments. Please delete or reassign the departments first.");
            }
            var result = await companyRepository.DeleteCompanyAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
