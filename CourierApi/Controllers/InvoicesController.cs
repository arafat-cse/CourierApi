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

        public InvoicesController(CourierDbContext db)
        {
            _db = db;
        }

        private readonly CommanResponse cp = new CommanResponse();

        // GET: api/Invoices
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            try
            {
                var invoices = await _db.Invoices
                    .Include(c => c.Customers)
                    .Include(p => p.PaymentMethods)
                    .Include(p => p.Parcels)
                    .ToListAsync();

                if (!invoices.Any())
                {
                    cp.status = false;
                    cp.message = "No invoices found.";
                    cp.content = null;
                    return Ok(cp);
                }

                cp.status = true;
                cp.message = "Invoices retrieved successfully.";
                cp.content = invoices;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving invoices.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // GET: api/Invoices/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            try
            {
                var invoice = await _db.Invoices
                    .Include(c => c.Customers)
                    .Include(p => p.PaymentMethods)
                    .Include(p => p.Parcels)
                    .FirstOrDefaultAsync(i => i.invoiceId == id);

                if (invoice == null)
                {
                    cp.status = false;
                    cp.message = "Invoice not found.";
                    return NotFound(cp);
                }

                cp.status = true;
                cp.message = "Invoice retrieved successfully.";
                cp.content = invoice;
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Error occurred while retrieving the invoice.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // POST: api/Invoices
        [HttpPost]
        public async Task<IActionResult> PostInvoice(Invoice invoice)
        {
            try
            {
                var isValidCustomer = await _db.Customers.AnyAsync(d => d.customerId == invoice.customerId);
                var isValidPaymentMethod = await _db.PaymentMethods.AnyAsync(d => d.paymentMethodId == invoice.paymentMethodId);
                var isValidParcel = await _db.Parcels.AnyAsync(d => d.parcelId == invoice.ParcelsId);

                if (!isValidCustomer || !isValidPaymentMethod || !isValidParcel)
                {
                    cp.status = false;
                    cp.message = "One or more dependencies are invalid.";
                    return BadRequest(cp);
                }

                _db.Invoices.Add(invoice);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Invoice created successfully.";
                cp.content = invoice;
                return CreatedAtAction(nameof(GetInvoice), new { id = invoice.invoiceId }, cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Failed to create a new invoice.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // PUT: api/Invoices/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.invoiceId)
            {
                cp.status = false;
                cp.message = "Invoice ID mismatch.";
                return BadRequest(cp);
            }

            _db.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                cp.status = true;
                cp.message = "Invoice updated successfully.";
                return Ok(cp);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    cp.status = false;
                    cp.message = "Invoice not found.";
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
                cp.message = "Failed to update the invoice.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        // DELETE: api/Invoices/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                var invoice = await _db.Invoices.FindAsync(id);

                if (invoice == null)
                {
                    cp.status = false;
                    cp.message = "Invoice not found.";
                    return NotFound(cp);
                }

                _db.Invoices.Remove(invoice);
                await _db.SaveChangesAsync();

                cp.status = true;
                cp.message = "Invoice deleted successfully.";
                return Ok(cp);
            }
            catch (Exception ex)
            {
                cp.status = false;
                cp.message = "Failed to delete the invoice.";
                cp.errorMessage = ex.Message;
                return StatusCode(500, cp);
            }
        }

        private bool InvoiceExists(int id)
        {
            return _db.Invoices.Any(e => e.invoiceId == id);
        }
    }

}
