namespace CompanyAPI.Model.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}
