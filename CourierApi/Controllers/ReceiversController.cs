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
    public class ReceiversController : ControllerBase
    {
        private readonly CourierDbContext _context;

        public ReceiversController(CourierDbContext context)
        {
            _context = context;
        }

        // GET: api/Receivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receiver>>> GetReceivers()
        {
            return await _context.Receivers.ToListAsync();
        }

        // GET: api/Receivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receiver>> GetReceiver(int id)
        {
            var receiver = await _context.Receivers.FindAsync(id);

            if (receiver == null)
            {
                return NotFound();
            }

            return receiver;
        }

        // PUT: api/Receivers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiver(int id, Receiver receiver)
        {
            if (id != receiver.receiverId)
            {
                return BadRequest();
            }

            _context.Entry(receiver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiverExists(id))
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

        // POST: api/Receivers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receiver>> PostReceiver(Receiver receiver)
        {
            _context.Receivers.Add(receiver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceiver", new { id = receiver.receiverId }, receiver);
        }

        // DELETE: api/Receivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiver(int id)
        {
            var receiver = await _context.Receivers.FindAsync(id);
            if (receiver == null)
            {
                return NotFound();
            }

            _context.Receivers.Remove(receiver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiverExists(int id)
        {
            return _context.Receivers.Any(e => e.receiverId == id);
        }
    }
}
