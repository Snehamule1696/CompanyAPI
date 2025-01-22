using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.Model.Domain
{
    public class Company
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Company Name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Company Location is required")]
        public string Location { get; set; }
    }
}
