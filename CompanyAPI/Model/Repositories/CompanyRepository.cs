using AutoMapper;
using CompanyAPI.Data;
using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyAPI.Model.Repositories
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly CompanyDbContext context;
        private readonly IMapper mapper;

        public CompanyRepository(CompanyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<CompanyDTO>> GetCompanyDetailsAsync()
        {
            var recordsCompany = await context.Companies
                                .ToListAsync();
            var recordDtos = recordsCompany.Select(r => new CompanyDTO
            {
                Id = r.Id,
                Name=r.Name,
                Location=r.Location
            }).ToList();
            var companyDataList = mapper.Map<List<CompanyDTO>>(recordDtos);
            return companyDataList;
        }
        public async Task<CompanyDTO> GetCompanyByIDAsync(int id)
        {
            var record = await context.Companies
                              .FirstOrDefaultAsync(x => x.Id == id);

            if (record == null)
            {
                return null;  // Or throw an exception if you prefer
            }

            // Map the retrieved record to a DTO
            var recordDto = new CompanyDTO
            {
                Id = record.Id,
                Name = record.Name,
                Location = record.Location
            };
            var companyData = mapper.Map<CompanyDTO>(recordDto);//Mapper
            return (companyData);
        }
        public async Task<CompanyDTO> CreateCompanyAsync(Company company)
        {

            await context.Companies.AddAsync(company);
            await context.SaveChangesAsync();

            var companyData = mapper.Map<CompanyDTO>(company);//Mapper
            return (companyData);
        }

        // Update an existing MentalHealthRecord by ID
        public async Task<CompanyDTO> UpdateCompanyAsync(int id, Company company)
        {
            var existingRecord = await context.Companies.FindAsync(id);

            if (existingRecord == null)
            {
                return null;  // Or throw an exception if you want
            }

            // Update the fields
                existingRecord.Name = company.Name;
                existingRecord.Location = company.Location;
            

            // Save the changes
            await context.SaveChangesAsync();
            var companyData = mapper.Map<CompanyDTO>(company);//Mapper
            return (companyData);
        }

        // Delete a MentalHealthRecord by ID
        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var record = await context.Companies.FindAsync(id);

            if (record == null)
            {
                return false;  // Not found
            }

            context.Companies.Remove(record);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
