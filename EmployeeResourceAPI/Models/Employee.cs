namespace EmployeeResourceAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Admin, Manager, Employee
        public string Designation { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation property
        public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
    }
}
