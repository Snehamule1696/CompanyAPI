using CompanyAPI.Model.Domain;
using CompanyAPI.Model.DTO;

namespace CompanyAPI.Model.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDTO>> GetDepartmentDetailsAsync();
        Task<DepartmentDTO> GetDepartmentByIDAsync(int id);
        Task<DepartmentDTO> CreateDepartmentAsync(Department department);
        Task<DepartmentDTO> UpdateDepartmentAsync(int id, Department department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
