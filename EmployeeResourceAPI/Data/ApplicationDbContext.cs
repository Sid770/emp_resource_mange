using Microsoft.EntityFrameworkCore;
using EmployeeResourceAPI.Models;

namespace EmployeeResourceAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Allocation> Allocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Department).HasMaxLength(50);
                entity.Property(e => e.Role).HasMaxLength(20);
                entity.Property(e => e.Designation).HasMaxLength(50);
            });

            // Configure Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Status).HasMaxLength(20);
                entity.Property(p => p.ManagerName).HasMaxLength(100);
                entity.Property(p => p.ClientName).HasMaxLength(100);
            });

            // Configure Allocation
            modelBuilder.Entity<Allocation>(entity =>
            {
                entity.HasKey(a => a.Id);
                
                entity.HasOne(a => a.Employee)
                    .WithMany(e => e.Allocations)
                    .HasForeignKey(a => a.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Project)
                    .WithMany(p => p.Allocations)
                    .HasForeignKey(a => a.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(a => a.Remarks).HasMaxLength(200);
            });

            // Seed initial data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "John Admin", Email = "admin@company.com", Phone = "1234567890", Department = "IT", Role = "Admin", Designation = "System Administrator", JoiningDate = DateTime.Now.AddYears(-3), IsActive = true },
                new Employee { Id = 2, Name = "Sarah Manager", Email = "manager@company.com", Phone = "2345678901", Department = "IT", Role = "Manager", Designation = "Project Manager", JoiningDate = DateTime.Now.AddYears(-2), IsActive = true },
                new Employee { Id = 3, Name = "Mike Developer", Email = "mike@company.com", Phone = "3456789012", Department = "IT", Role = "Employee", Designation = "Senior Developer", JoiningDate = DateTime.Now.AddYears(-1), IsActive = true },
                new Employee { Id = 4, Name = "Lisa Designer", Email = "lisa@company.com", Phone = "4567890123", Department = "Design", Role = "Employee", Designation = "UI/UX Designer", JoiningDate = DateTime.Now.AddMonths(-6), IsActive = true }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Employee Management System", Description = "Internal HRMS system", StartDate = DateTime.Now.AddMonths(-3), Status = "Active", ManagerName = "Sarah Manager", ClientName = "Internal" },
                new Project { Id = 2, Name = "E-Commerce Platform", Description = "Online shopping platform", StartDate = DateTime.Now.AddMonths(-2), Status = "Active", ManagerName = "Sarah Manager", ClientName = "TechMart Inc." }
            );

            modelBuilder.Entity<Allocation>().HasData(
                new Allocation { Id = 1, EmployeeId = 3, ProjectId = 1, AllocationDate = DateTime.Now.AddMonths(-3), AllocationPercentage = 80, Remarks = "Lead Developer" },
                new Allocation { Id = 2, EmployeeId = 4, ProjectId = 1, AllocationDate = DateTime.Now.AddMonths(-2), AllocationPercentage = 50, Remarks = "UI Design" },
                new Allocation { Id = 3, EmployeeId = 3, ProjectId = 2, AllocationDate = DateTime.Now.AddMonths(-1), AllocationPercentage = 20, Remarks = "Backend Support" }
            );
        }
    }
}
