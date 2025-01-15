using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;

namespace CompanyAPI.Model.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<CompanyDTO>> GetCompanyDetailsAsync();
        Task<CompanyDTO> GetCompanyByIDAsync(int id);
        Task<CompanyDTO> CreateCompanyAsync(Company company);
        Task<CompanyDTO> UpdateCompanyAsync(int id, Company company);
        Task<bool> DeleteCompanyAsync(int id);
    }
}
