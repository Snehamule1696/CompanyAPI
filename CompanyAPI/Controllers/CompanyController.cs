using CompanyAPI.Data;
using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;
using CompanyAPI.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDbContext companyDbContext;
        private readonly ICompanyRepository companyRepository;

        public CompanyController(CompanyDbContext companyDbContext,ICompanyRepository companyRepository)
        {
            this.companyDbContext = companyDbContext;
            this.companyRepository = companyRepository;
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
            var createdRecord = await companyRepository.CreateCompanyAsync(company);

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
            var result = await companyRepository.DeleteCompanyAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
