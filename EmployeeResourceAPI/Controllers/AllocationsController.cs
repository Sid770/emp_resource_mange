using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeResourceAPI.Data;
using EmployeeResourceAPI.Models;
using EmployeeResourceAPI.DTOs;

namespace EmployeeResourceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Allocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllocationDto>>> GetAllocations()
        {
            var allocations = await _context.Allocations
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Select(a => new AllocationDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.Name,
                    ProjectId = a.ProjectId,
                    ProjectName = a.Project.Name,
                    AllocationDate = a.AllocationDate,
                    ReleaseDate = a.ReleaseDate,
                    AllocationPercentage = a.AllocationPercentage,
                    Remarks = a.Remarks
                })
                .ToListAsync();

            return Ok(allocations);
        }

        // GET: api/Allocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllocationDto>> GetAllocation(int id)
        {
            var allocation = await _context.Allocations
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (allocation == null)
            {
                return NotFound();
            }

            var allocationDto = new AllocationDto
            {
                Id = allocation.Id,
                EmployeeId = allocation.EmployeeId,
                EmployeeName = allocation.Employee.Name,
                ProjectId = allocation.ProjectId,
                ProjectName = allocation.Project.Name,
                AllocationDate = allocation.AllocationDate,
                ReleaseDate = allocation.ReleaseDate,
                AllocationPercentage = allocation.AllocationPercentage,
                Remarks = allocation.Remarks
            };

            return Ok(allocationDto);
        }

        // GET: api/Allocations/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AllocationDto>>> GetEmployeeAllocations(int employeeId)
        {
            var allocations = await _context.Allocations
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Where(a => a.EmployeeId == employeeId)
                .Select(a => new AllocationDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.Name,
                    ProjectId = a.ProjectId,
                    ProjectName = a.Project.Name,
                    AllocationDate = a.AllocationDate,
                    ReleaseDate = a.ReleaseDate,
                    AllocationPercentage = a.AllocationPercentage,
                    Remarks = a.Remarks
                })
                .ToListAsync();

            return Ok(allocations);
        }

        // GET: api/Allocations/project/5
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<AllocationDto>>> GetProjectAllocations(int projectId)
        {
            var allocations = await _context.Allocations
                .Include(a => a.Employee)
                .Include(a => a.Project)
                .Where(a => a.ProjectId == projectId)
                .Select(a => new AllocationDto
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.Name,
                    ProjectId = a.ProjectId,
                    ProjectName = a.Project.Name,
                    AllocationDate = a.AllocationDate,
                    ReleaseDate = a.ReleaseDate,
                    AllocationPercentage = a.AllocationPercentage,
                    Remarks = a.Remarks
                })
                .ToListAsync();

            return Ok(allocations);
        }

        // POST: api/Allocations
        [HttpPost]
        public async Task<ActionResult<AllocationDto>> CreateAllocation(CreateAllocationDto createDto)
        {
            var allocation = new Allocation
            {
                EmployeeId = createDto.EmployeeId,
                ProjectId = createDto.ProjectId,
                AllocationDate = createDto.AllocationDate,
                ReleaseDate = createDto.ReleaseDate,
                AllocationPercentage = createDto.AllocationPercentage,
                Remarks = createDto.Remarks
            };

            _context.Allocations.Add(allocation);
            await _context.SaveChangesAsync();

            // Load navigation properties
            await _context.Entry(allocation).Reference(a => a.Employee).LoadAsync();
            await _context.Entry(allocation).Reference(a => a.Project).LoadAsync();

            var allocationDto = new AllocationDto
            {
                Id = allocation.Id,
                EmployeeId = allocation.EmployeeId,
                EmployeeName = allocation.Employee.Name,
                ProjectId = allocation.ProjectId,
                ProjectName = allocation.Project.Name,
                AllocationDate = allocation.AllocationDate,
                ReleaseDate = allocation.ReleaseDate,
                AllocationPercentage = allocation.AllocationPercentage,
                Remarks = allocation.Remarks
            };

            return CreatedAtAction(nameof(GetAllocation), new { id = allocation.Id }, allocationDto);
        }

        // PUT: api/Allocations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAllocation(int id, UpdateAllocationDto updateDto)
        {
            var allocation = await _context.Allocations.FindAsync(id);

            if (allocation == null)
            {
                return NotFound();
            }

            allocation.ReleaseDate = updateDto.ReleaseDate;
            allocation.AllocationPercentage = updateDto.AllocationPercentage;
            allocation.Remarks = updateDto.Remarks;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllocationExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Allocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllocation(int id)
        {
            var allocation = await _context.Allocations.FindAsync(id);
            if (allocation == null)
            {
                return NotFound();
            }

            _context.Allocations.Remove(allocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AllocationExists(int id)
        {
            return _context.Allocations.Any(e => e.Id == id);
        }
    }
}
