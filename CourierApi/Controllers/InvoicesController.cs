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
    public class InvoicesController : ControllerBase
    {
        private readonly CourierDbContext _db;
        private readonly CommanResponse cp = new CommanResponse();

        public InvoicesController(CourierDbContext db)
        {
            _db = db;
        }
        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            try
            {
                var invoices = await _db.Invoices.ToListAsync();
                if (invoices == null || !invoices.Any())
                {
                    cp.errorMessage = null;
                    cp.status = true;
                    cp.message = "No invoices found.";
                    cp.content = null;
                    return Ok(cp);
                }

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Invoices retrieved successfully!";
                cp.content = invoices;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving invoices.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            try
            {
                var invoice = await _db.Invoices.FindAsync(id);
                if (invoice == null)
                {
                    cp.errorMessage = "Invoice not found";
                    cp.status = false;
                    cp.message = "No invoice exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }
                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Invoice retrieved successfully!";
                cp.content = invoice;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while retrieving the invoice.";
                cp.content = null;
                return BadRequest(cp);
            }
        }
        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.invoiceId)
            {
                cp.errorMessage = "Bad request, ID mismatch.";
                cp.status = false;
                cp.message = "Invoice not found.";
                cp.content = null;
                return BadRequest(cp);
            }
            _db.Entry(invoice).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Invoices.Any(i => i.invoiceId == id))
                {
                    return NotFound(new { Message = "Invoice not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { Message = "Invoice updated successfully!", invoiceId = id });
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                cp.errorMessage = "Invalid model state.";
                cp.status = false;
                cp.message = "Please check the input data.";
                cp.content = ModelState;
                return BadRequest(cp);
            }
            try
            {
                _db.Invoices.Add(invoice);
                await _db.SaveChangesAsync();

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Invoice created successfully!";
                cp.content = invoice;

                return CreatedAtAction("GetInvoice", new { id = invoice.invoiceId }, invoice);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "Failed to create invoice.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                var invoice = await _db.Invoices.FindAsync(id);

                if (invoice == null)
                {
                    cp.errorMessage = "Invoice not found.";
                    cp.status = false;
                    cp.message = "No invoice exists with the provided ID.";
                    cp.content = null;
                    return NotFound(cp);
                }

                _db.Invoices.Remove(invoice);
                await _db.SaveChangesAsync();

                cp.errorMessage = null;
                cp.status = true;
                cp.message = "Invoice deleted successfully!";
                cp.content = invoice;

                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.errorMessage = ex.Message;
                cp.status = false;
                cp.message = "An error occurred while deleting the invoice.";
                cp.content = null;
                return BadRequest(cp);
            }
        }

        private bool InvoiceExists(int id)
        {
            return _db.Invoices.Any(e => e.invoiceId == id);
        }
    }
}
