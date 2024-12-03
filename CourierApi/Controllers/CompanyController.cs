using CourierApi.Data;
using CourierApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CourierDbContext _db;

        public CompanyController(CourierDbContext db)
        {
            _db=db;
        }
        //1. Get All Companys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            return await _db.Companys.ToListAsync();
        }
        // 2. GET a Company by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _db.Companys.FindAsync(id);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            return company;
        }
        // 3. POST a New Company
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _db.Companys.Add(company);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompany), new { id = company.companyId }, company);
        }
        // 4. PUT Update a Company
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.companyId)
            {
                return BadRequest("Company ID mismatch");
            }

            _db.Entry(company).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Companys.Any(c => c.companyId == id))
                {
                    return NotFound("Company not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE a Company (Optional if required)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _db.Companys.FindAsync(id);

            if (company == null)
            {
                return NotFound("Company not found");
            }

            _db.Companys.Remove(company);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
