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
        private readonly CommanResponse cp = new CommanResponse();

        public BranchesController(CourierDbContext db)
        {
            _db = db;
        }

        // GET: api/Branches
        [HttpGet]
        public IActionResult GetBranches()
        {
            try
            {
                var branches = _db.Branches
                    .Include(b => b.ChildBranches)
                    .ToList();

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

                if (!branchDTOs.Any())
                {
                    cp.status = false;
                    cp.message = "No branches found.";
                    cp.content = null;
                    return Ok(cp);
                }

                cp.status = true;
                cp.message = "Branches retrieved successfully.";
                cp.content = branchDTOs;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving branches.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {
            try
            {
                var branch = await _db.Branches
                    .Include(b => b.ChildBranches)
                    .FirstOrDefaultAsync(b => b.branchId == id);

                if (branch == null)
                {
                    cp.status = false;
                    cp.message = "Branch not found.";
                    cp.content = null;
                    return NotFound(cp);
                }

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

                cp.status = true;
                cp.message = "Branch retrieved successfully.";
                cp.content = branchDto;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving the branch.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // POST: api/Branches
        [HttpPost]
        public async Task<IActionResult> PostBranch(Branch branch)
        {
            try
            {
                if (branch.ParentId.HasValue)
                {
                    var parentBranch = await _db.Branches.FindAsync(branch.ParentId.Value);
                    if (parentBranch == null)
                    {
                        cp.status = false;
                        cp.message = "Invalid ParentId.";
                        return BadRequest(cp);
                    }
                }

                _db.Branches.Add(branch);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Branch created successfully.";
                cp.content = branch;
                return CreatedAtAction(nameof(GetBranch), new { id = branch.branchId }, cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while creating the branch.";
                cp.errorMessage = ex.Message;
                cp.content = null;
                return StatusCode(500, cp);
            }
        }

        // PUT: api/Branches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, Branch branch)
        {
            if (id != branch.branchId)
            {
                cp.status = false;
                cp.message = "Branch ID mismatch.";
                return BadRequest(cp);
            }

            try
            {
                if (branch.ParentId.HasValue && branch.ParentId != id)
                {
                    var parentBranch = await _db.Branches.FindAsync(branch.ParentId.Value);
                    if (parentBranch == null)
                    {
                        cp.status = false;
                        cp.message = "Invalid ParentId.";
                        return BadRequest(cp);
                    }
                }

                _db.Entry(branch).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Branch updated successfully.";
                return Ok(cp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    cp.status = false;
                    cp.message = "Branch not found.";
                    return NotFound(cp);
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while updating the branch.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                var branch = await _db.Branches
                    .Include(b => b.ChildBranches)
                    .FirstOrDefaultAsync(b => b.branchId == id);

                if (branch == null)
                {
                    cp.status = false;
                    cp.message = "Branch not found.";
                    return NotFound(cp);
                }

                if (branch.ChildBranches != null && branch.ChildBranches.Any())
                {
                    cp.status = false;
                    cp.message = "Cannot delete a branch that has child branches.";
                    return BadRequest(cp);
                }

                _db.Branches.Remove(branch);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Branch deleted successfully.";
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while deleting the branch.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        private bool BranchExists(int id)
        {
            return _db.Branches.Any(e => e.branchId == id);
        }
    }
}
