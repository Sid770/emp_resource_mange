namespace EmployeeResourceAPI.DTOs
{
    public class AllocationDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime AllocationDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int AllocationPercentage { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }

    public class CreateAllocationDto
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public DateTime AllocationDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int AllocationPercentage { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }

    public class UpdateAllocationDto
    {
        public DateTime? ReleaseDate { get; set; }
        public int AllocationPercentage { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
