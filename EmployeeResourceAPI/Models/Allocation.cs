namespace EmployeeResourceAPI.Models
{
    public class Allocation
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public DateTime AllocationDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int AllocationPercentage { get; set; } // 0-100
        public string Remarks { get; set; } = string.Empty;

        // Navigation properties
        public Employee Employee { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}
