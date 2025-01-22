using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.Model.Domain
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
