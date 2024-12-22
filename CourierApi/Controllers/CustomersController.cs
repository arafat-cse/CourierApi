using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourierApi.Data;
using CourierApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace CourierApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CourierDbContext _db;
        public CustomersController(CourierDbContext db)
        {
            _db = db;
        }
        //CommanResponse
        private readonly CommanResponse cp = new CommanResponse(); 
        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {

                var customer = await _db.Customers.ToListAsync();
                if (customer == null || !customer.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No customer found here.";
                    cp.content = null;
                    return Ok(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Customer retrieved successfully!";
                cp.content = customer;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the Customer.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // GET:/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                // Find the Customer by ID
                var customer = await _db.Customers.FindAsync(id);
                if (customer == null)
                {
                    cp.errorMessage = "customer is not found";
                    cp.status = false;
                    cp.message = "No customer exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "customer retrieved successfully!";
                cp.content = customer;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the customer.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // PUT:/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.customerId)
            {
                cp.errorMessage = "Badrequer ID mismatch";
                cp.status = false;
                cp.message = "customer not found";
                cp.content = null;
                return BadRequest(cp);
            }
            _db.Entry(customer).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Customers.Any(c => c.customerId == id))
                {
                    return NotFound("customer not found");
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { Message = "customer updated successfully", customerId = id });
        }
        //POST:
       [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                 await _db.SaveChangesAsync();
                cp.errorMessage = null; 
                cp.status = true;
                cp.message = "customer created successfully!";
                cp.content = customer;
                return CreatedAtAction("GetCustomer", new { id = customer.customerId }, customer);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create customer.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // DELETE:/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                // Find the customer by ID
                var customer = await _db.Customers.FindAsync(id);

                if (customer == null)
                {
                    cp.errorMessage = "customer not found";
                    cp.status = false;
                    cp.message = "No customer exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Customer deleted successfully!";
                cp.content = customer;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the Customer.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        private bool CustomerExists(int id)
        {
            return _db.Customers.Any(e => e.customerId == id);
        }
    }
}
