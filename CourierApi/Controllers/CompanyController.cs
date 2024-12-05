using CourierApi.Data;
using CourierApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        //CommanResponse
        CommanResponse cp = new CommanResponse();

        //1. Get All Companys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            return await _db.Companys.ToListAsync();
        }
        // 2. GET a Company by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CommanResponse>> GetCompany(int id)
        {
            try
            {
                var company = await _db.Companys.FindAsync(id);

                if (company == null)
                {
                    cp.errorMessage = "Company not found";
                    cp.status = false;
                    cp.message = "No company exists with the provided ID.";
                    cp.content = null;

                    return NotFound(cp); // Return NotFound with the common response
                }

                // Populate response for a successful find
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Company retrieved successfully!";
                cp.content = company.ToJson();

                return Ok(cp); // Return OK with the common response
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the company.";
                cp.content = null;

                return BadRequest(cp); // Return BadRequest with the common response in case of failure
            }
        }
        // 3. POST a New Company
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            try
            {
                _db.Companys.Add(company);
                await _db.SaveChangesAsync();

                cp.errorMessage = null; // No error since the operation is successful
                cp.status = true; // Success status
                cp.message = "Company created successfully!";
                cp.content = company.ToJson();

                // Returning the common response with CreatedAtAction
                return CreatedAtAction(nameof(GetCompany), new { id = company.companyId }, cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create company.";
                cp.content = null;

                return BadRequest(cp); // Return BadRequest with the common response in case of failure
            }

        }
        // 4. PUT Update a Company
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
           
            if (id != company.companyId)
            {
                cp.errorMessage="Badrequer ID mismatch";
                cp.status = false;
                cp.message = "company not found";
                cp.content = null;
                return BadRequest(cp);
               /* return BadRequest("Company ID mismatch");*/
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

            return Ok(new { Message = "Company updated successfully", CompanyId = id });
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

            return Ok(new { Message = "Company Delete successfully", CompanyId = id });
        }

    }
}
