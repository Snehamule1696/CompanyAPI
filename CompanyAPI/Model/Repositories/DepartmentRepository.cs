using AutoMapper;
using CompanyAPI.Data;
using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Model.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly CompanyDbContext context;
        private readonly IMapper mapper;

        public DepartmentRepository(CompanyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<DepartmentDTO>> GetDepartmentDetailsAsync()
        {
           var recordsCompany = await context.Departments.ToListAsync();
          //var recordsCompany = await context.Departments.Include(r => r.CompanyId)  
          //                       .ToListAsync();
            //var recordDtos = recordsCompany.Select(r => new DepartmentDTO
            //{
            //    Id = r.Id,
            //    Name = r.Name,
            //    CompanyId =r.CompanyId
            //}).ToList();
            var departmentDataList = mapper.Map<List<DepartmentDTO>>(recordsCompany);
            return departmentDataList;
        }
        public async Task<DepartmentDTO> GetDepartmentByIDAsync(int id)
        {
            var record = await context.Departments
                              .FirstOrDefaultAsync(x => x.Id == id);

            if (record == null)
            {
                return null;  // Or throw an exception if you prefer
            }

            // Map the retrieved record to a DTO
            var recordDto = new DepartmentDTO
            {
                Id = record.Id,
                Name = record.Name,
                CompanyId = record.CompanyId
            };
            var departmentData = mapper.Map<DepartmentDTO>(recordDto);//Mapper
            return (departmentData);
        }
        public async Task<DepartmentDTO> GetByCompanyIdAsync(int companyId)
        {
            var record = await context.Departments
                              .FirstOrDefaultAsync(x => x.CompanyId == companyId);

            if (record == null)
            {
                return null;  // Or throw an exception if you prefer
            }

            // Map the retrieved record to a DTO
            var recordDto = new DepartmentDTO
            {
                Id = record.Id,
                Name = record.Name,
                CompanyId = record.CompanyId
            };
            var departmentData = mapper.Map<DepartmentDTO>(recordDto);//Mapper
            return (departmentData);
        }
        public async Task<DepartmentDTO> GetDepartmentByNameAsync(string name)
        {
            var record = await context.Departments.FirstOrDefaultAsync(c => c.Name == name);
            var recordDto = new DepartmentDTO
            {
                Id = record.Id,
                Name = record.Name,
                CompanyId = record.CompanyId
            };
            var departmentData = mapper.Map<DepartmentDTO>(recordDto);//Mapper
            return (departmentData);
        }
        public async Task<DepartmentDTO> CreateDepartmentAsync(Department department)
        {

            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            var departmentData = mapper.Map<DepartmentDTO>(department);//Mapper
            return (departmentData);
        }

        // Update an existing MentalHealthRecord by ID
        public async Task<DepartmentDTO> UpdateDepartmentAsync(int id, Department department)
        {
            var existingRecord = await context.Departments.FindAsync(id);

            if (existingRecord == null)
            {
                return null;  // Or throw an exception if you want
            }

            // Update the fields
            existingRecord.Name = department.Name;
            existingRecord.CompanyId = department.CompanyId;


            // Save the changes
            await context.SaveChangesAsync();
            var departmentData = mapper.Map<DepartmentDTO>(department);//Mapper
            return (departmentData);
        }

        // Delete a MentalHealthRecord by ID
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var record = await context.Departments.FindAsync(id);

            if (record == null)
            {
                return false;  // Not found
            }

            context.Departments.Remove(record);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
