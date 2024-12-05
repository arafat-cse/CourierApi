using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierApi.Data;
using CourierApi.Models;

namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public BranchesController(CourierDbContext db)
        {
            _db = db;
        }
        // GET: api/Branches
        [HttpGet]
        public IActionResult GetBranches()
        {
            // Branches All ChildBranches loading
            var branches = _db.Branches
                                     .Include(b => b.ChildBranches)
                                     .ToList();

            // Entity to DTO- Data Convert
            var branchDTOs = branches.Select(b => new BranchDTO
            {
                BranchId = b.branchId,
                BranchName = b.branchName,
                Address = b.address,
                ParentId = b.ParentId,
                IsActive = b.IsActive,
                ChildBranches = b.ChildBranches?.Select(cb => new BranchDTO
                {
                    BranchId = cb.branchId,
                    BranchName = cb.branchName,
                    Address = cb.address,
                    ParentId = cb.ParentId,
                    IsActive = cb.IsActive
                }).ToList()
            }).ToList();

            return Ok(branchDTOs);
        }
       
        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDTO>> GetBranch(int id)
        {
            var branch = await _db.Branches
                .Include(b => b.ChildBranches) // Include Child branches
                .FirstOrDefaultAsync(b => b.branchId == id);

            if (branch == null)
            {
                return NotFound();
            }

            // Map to DTO
            var branchDto = new BranchDTO
            {
                BranchId = branch.branchId,
                BranchName = branch.branchName,
                Address = branch.address,
                ParentId = branch.ParentId,
                IsActive = branch.IsActive,
                ChildBranches = branch.ChildBranches?.Select(cb => new BranchDTO
                {
                    BranchId = cb.branchId,
                    BranchName = cb.branchName,
                    Address = cb.address,
                    ParentId = cb.ParentId,
                    IsActive = cb.IsActive
                }).ToList()
            };

            return Ok(branchDto);
        }
        // POST: api/Branches
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
            // Check if Parent exists if ParentId is provided
            if (branch.ParentId.HasValue)
            {
                var parentBranch = await _db.Branches.FindAsync(branch.ParentId.Value);
                if (parentBranch == null)
                {
                    return BadRequest("Invalid ParentId.");
                }
            }

            _db.Branches.Add(branch);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetBranch", new { id = branch.branchId }, branch);
        }

        // PUT: api/Branches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, Branch branch)
        {
            if (id != branch.branchId)
            {
                return BadRequest();
            }

            // Check if new Parent exists if ParentId is updated
            if (branch.ParentId.HasValue && branch.ParentId != id)
            {
                var parentBranch = await _db.Branches.FindAsync(branch.ParentId.Value);
                if (parentBranch == null)
                {
                    return BadRequest("Invalid ParentId.");
                }
            }

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _db.Branches
                .Include(b => b.ChildBranches) // Load Child branches
                .FirstOrDefaultAsync(b => b.branchId == id);

            if (branch == null)
            {
                return NotFound();
            }

            // Prevent deletion if it has children
            if (branch.ChildBranches != null && branch.ChildBranches.Any())
            {
                return BadRequest("Cannot delete a branch that has child branches.");
            }

            _db.Branches.Remove(branch);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool BranchExists(int id)
        {
            return _db.Branches.Any(e => e.branchId == id);
        }
    }
}
