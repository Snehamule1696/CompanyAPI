using CompanyAPI.Data;
using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;
using CompanyAPI.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            var departmentData = await departmentRepository.GetDepartmentDetailsAsync();


            return departmentData;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRecord = await departmentRepository.GetDepartmentByNameAsync(department.Name);
            if (createdRecord!=null)
            {
                return BadRequest("A department with the same name already exists");
            }
            if (department.Name.Length < 3 || department.Name.Length > 100)
            {
                return BadRequest("Department name must be between 3 and 100 characters.");
            }
            if (!Regex.IsMatch(department.Name, @"^[a-zA-Z0-9\s\-_.]+$"))
            {
                return BadRequest("Department name can only contain letters, numbers, spaces, hyphens, and underscores.");
            }
            
            await departmentRepository.CreateDepartmentAsync(department);

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await departmentRepository.DeleteDepartmentAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
