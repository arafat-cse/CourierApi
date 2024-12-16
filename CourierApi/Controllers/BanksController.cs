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
    public class BanksController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public BanksController(CourierDbContext db)
        {
            _db = db;
        }
        //CommanResponse
        CommanResponse cp = new CommanResponse();
        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {

            try
            {
                var banks = await _db.Banks.Include(b => b.Company).ToListAsync();
                if (banks == null || !banks.Any())
                {
                    cp.errorMessage = "No banks found.";
                    cp.status = false;
                    cp.message = "No bank data available.";
                    cp.content = null;
                    return Ok(cp);
                }

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Banks retrieved successfully!";
                cp.content = banks;

                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the banks.";
                cp.content = null;

                return BadRequest(cp);
            }
        }

        // GET: api/Banks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            try
            {
                var bank = await _db.Banks.Include(b => b.Company).FirstOrDefaultAsync(b => b.bankId == id);

                if (bank == null)
                {
                    cp.errorMessage = "Bank not found.";
                    cp.status = false;
                    cp.message = "No bank exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Bank retrieved successfully!";
                cp.content = bank;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the bank.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // PUT: api/Banks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(int id, Bank bank)
        {
            if (id != bank.bankId)
            {
                cp.errorMessage = "Invalid bank ID.";
                cp.status = false;
                cp.message = "Bank ID mismatch.";
                cp.content = null;
                return BadRequest(cp);
            }

            var existingBank = await _db.Banks.FindAsync(id);
            if (existingBank == null)
            {
                cp.errorMessage = "Bank not found.";
                cp.status = false;
                cp.message = "No bank exists with the provided ID.";
                cp.content = null;
                return NotFound(cp);
            }

            existingBank.branchName = bank.branchName;
            existingBank.updateBy = bank.updateBy;
            existingBank.updateDate = DateTime.UtcNow;
            existingBank.companyId = bank.companyId;

            try
            {
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Bank updated successfully!";
                cp.content = null; // Optional
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while updating the bank.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // POST: api/Banks
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            try
            {
                _db.Banks.Add(bank);
                await _db.SaveChangesAsync();
             
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "New bank created successfully!";
                cp.content = bank;

                return CreatedAtAction(nameof(GetBank), new { id = bank.bankId }, cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create a new bank.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // DELETE: api/Banks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var bank = await _db.Banks.FindAsync(id);
            if (bank == null)
            {
                cp.errorMessage = "Bank not found.";
                cp.status = false;
                cp.message = "No bank exists with the provided ID.";
                cp.content = null;
                return NotFound(cp);
            }

            _db.Banks.Remove(bank);
            await _db.SaveChangesAsync();

            cp.errorMessage = null;
            cp.status = true;
            cp.message = "Bank deleted successfully!";
            cp.content = null;

            return Ok(cp);
        }

    }
}
