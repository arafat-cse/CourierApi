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
    public class BranchStaffsController : ControllerBase
    {
        private readonly CourierDbContext _context;

        public BranchStaffsController(CourierDbContext context)
        {
            _context = context;
        }

        // GET: api/BranchStaffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchStaff>>> GetBranchesStaffs()
        {
            return await _context.BranchesStaffs.ToListAsync();
        }

        // GET: api/BranchStaffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchStaff>> GetBranchStaff(int id)
        {
            var branchStaff = await _context.BranchesStaffs.FindAsync(id);

            if (branchStaff == null)
            {
                return NotFound();
            }

            return branchStaff;
        }

        // PUT: api/BranchStaffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranchStaff(int id, BranchStaff branchStaff)
        {
            if (id != branchStaff.branchStaffId)
            {
                return BadRequest();
            }

            _context.Entry(branchStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchStaffExists(id))
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

        // POST: api/BranchStaffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BranchStaff>> PostBranchStaff(BranchStaff branchStaff)
        {
            _context.BranchesStaffs.Add(branchStaff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBranchStaff", new { id = branchStaff.branchStaffId }, branchStaff);
        }

        // DELETE: api/BranchStaffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranchStaff(int id)
        {
            var branchStaff = await _context.BranchesStaffs.FindAsync(id);
            if (branchStaff == null)
            {
                return NotFound();
            }

            _context.BranchesStaffs.Remove(branchStaff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BranchStaffExists(int id)
        {
            return _context.BranchesStaffs.Any(e => e.branchStaffId == id);
        }
    }
}
