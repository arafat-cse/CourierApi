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

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _db.Invoices.ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.invoiceId)
            {
                return BadRequest();
            }

            _db.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.invoiceId }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return _db.Invoices.Any(e => e.invoiceId == id);
        }
    }
}
