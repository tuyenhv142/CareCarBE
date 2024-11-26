using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareCarAPI.Models;

namespace CareCarAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LienHeController : ControllerBase
    {
        private readonly CareCa1Context _context;

        public LienHeController(CareCa1Context context)
        {
            _context = context;
        }

        // GET: api/LienHe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LienHe>>> GetLienHes()
        {
          if (_context.LienHes == null)
          {
              return NotFound();
          }
            return await _context.LienHes.ToListAsync();
        }

        // GET: api/LienHe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LienHe>> GetLienHe(int id)
        {
          if (_context.LienHes == null)
          {
              return NotFound();
          }
            var lienHe = await _context.LienHes.FindAsync(id);

            if (lienHe == null)
            {
                return NotFound();
            }

            return lienHe;
        }

        // PUT: api/LienHe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLienHe(int id, LienHe lienHe)
        {
            if (id != lienHe.ContactId)
            {
                return BadRequest();
            }

            _context.Entry(lienHe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LienHeExists(id))
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

        // POST: api/LienHe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LienHe>> PostLienHe(LienHe lienHe)
        {
          if (_context.LienHes == null)
          {
              return Problem("Entity set 'CareCa1Context.LienHes'  is null.");
          }
            _context.LienHes.Add(lienHe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLienHe", new { id = lienHe.ContactId }, lienHe);
        }

        // DELETE: api/LienHe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLienHe(int id)
        {
            if (_context.LienHes == null)
            {
                return NotFound();
            }
            var lienHe = await _context.LienHes.FindAsync(id);
            if (lienHe == null)
            {
                return NotFound();
            }

            _context.LienHes.Remove(lienHe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LienHeExists(int id)
        {
            return (_context.LienHes?.Any(e => e.ContactId == id)).GetValueOrDefault();
        }
    }
}
