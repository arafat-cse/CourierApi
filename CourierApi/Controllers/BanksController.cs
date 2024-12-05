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

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            return await _db.Banks.ToListAsync();
        }

        // GET: For Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = await _db.Banks.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
        }

        // PUT: For Id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(int id, Bank bank)
        {
            if (id != bank.bankId)
            {
                return BadRequest();
            }

            _db.Entry(bank).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Bank Update successfully", bankId = id });
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            _db.Banks.Add(bank);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetBank", new { id = bank.bankId }, bank);
        }

        // DELETE: For Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var bank = await _db.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            _db.Banks.Remove(bank);
            await _db.SaveChangesAsync();

            return Ok(new { Message = "Bank Delete successfully", bankId = id });

        }

        private bool BankExists(int id)
        {
            return _db.Banks.Any(e => e.bankId == id);
        }
    }
}
