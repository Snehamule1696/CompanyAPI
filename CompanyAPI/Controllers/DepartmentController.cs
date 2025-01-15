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
    public class DepartmentController : ControllerBase
    {
        private readonly CompanyDbContext companyDbContext;
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(CompanyDbContext companyDbContext, IDepartmentRepository departmentRepository)
        {
            this.companyDbContext = companyDbContext;
            this.departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDTO>>> GetAllDepartmentDetails()
        {

            var companyData = await departmentRepository.GetDepartmentDetailsAsync();


            return companyData;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartmentById(int id)
        {
            var recordDto = await departmentRepository.GetDepartmentByIDAsync(id);

            if (recordDto == null)
            {
                return NotFound();
            }

            return Ok(recordDto);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartmentAsync(Department department)
        {
            var createdRecord = await departmentRepository.CreateDepartmentAsync(department);

            var createdRecordDto = new DepartmentDTO
            {
                Id = createdRecord.Id,
                Name = createdRecord.Name,
                CompanyId = createdRecord.CompanyId,
            };

            return createdRecordDto;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentDTO>> UpdateDepartmentAsync(int id, Department department)
        {
            var updatedRecord = await departmentRepository.UpdateDepartmentAsync(id, department);

            if (updatedRecord == null)
            {
                return NotFound();
            }

            return Ok(updatedRecord);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartmentAsync(int id)
        {
            var result = await departmentRepository.DeleteDepartmentAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
