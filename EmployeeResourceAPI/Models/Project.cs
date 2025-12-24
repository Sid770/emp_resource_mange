namespace EmployeeResourceAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = string.Empty; // Active, Completed, On Hold
        public string ManagerName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
    }
}
