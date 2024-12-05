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
        public async Task<ActionResult<CommanResponse>> GetCompany()
        {
            try
            {
               
                var company = await _db.Companys.ToListAsync(); 
                if (company == null || !company.Any())
                {
                    cp.errorMessage = null; 
                    cp.status = true;
                    cp.message = "No companies found.";
                    cp.content = null;
                    return Ok(cp); 
                }

                // Populate response for a successful find
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Companies retrieved successfully!";
                cp.content = company.ToJson(); 

                return Ok(cp); 
            }
            catch (Exception ex)
            {   
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the companies.";
                cp.content = null;

                return BadRequest(cp); 
            }
        }

        // 2. GET a Company by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CommanResponse>> GetCompany(int id)
        {

            try
            {
                // Find the company by ID
                var company = await _db.Companys.FindAsync(id);

                if (company == null)
                {
                    cp.errorMessage = "Company not found";
                    cp.status = false;
                    cp.message = "No company exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp); 
                }

                // Populate response for a successful retrieval
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Company retrieved successfully!";
                cp.content = company.ToJson(); 
                return Ok(cp); 
            }
            catch (Exception ex)
            {
                // Handle exceptions
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the company.";
                cp.content = null;
                return BadRequest(cp); 
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
                return BadRequest(cp); 
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
   
        // DELETE a Company
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommanResponse>> DeleteCompany(int id)
        {
            CommanResponse cp = new CommanResponse();

            try
            {
                // Find the company by ID
                var company = await _db.Companys.FindAsync(id);

                if (company == null)
                {
                    // Company not found response
                    cp.errorMessage = "Company not found";
                    cp.status = false;
                    cp.message = "No company exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp); 
                }

                // Remove the company and save changes
                _db.Companys.Remove(company);
                await _db.SaveChangesAsync();

                // Populate success response
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Company deleted successfully!";
                cp.content = company.ToJson();
                return Ok(cp); 
            }
            catch (Exception ex)
            {  
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the company.";
                cp.content = null;
                return BadRequest(cp); 
            }
        }

    }
}
