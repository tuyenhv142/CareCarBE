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
    public class TrangThaiController : ControllerBase
    {
        private readonly CareCa1Context _context;

        public TrangThaiController(CareCa1Context context)
        {
            _context = context;
        }

        // GET: api/TrangThai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrangThai>>> GetTrangThais()
        {
          if (_context.TrangThais == null)
          {
              return NotFound();
          }
            return await _context.TrangThais.ToListAsync();
        }

        // GET: api/TrangThai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrangThai>> GetTrangThai(int id)
        {
          if (_context.TrangThais == null)
          {
              return NotFound();
          }
            var trangThai = await _context.TrangThais.FindAsync(id);

            if (trangThai == null)
            {
                return NotFound();
            }

            return trangThai;
        }

        // PUT: api/TrangThai/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrangThai(int id, TrangThai trangThai)
        {
            if (id != trangThai.TrangThaiId)
            {
                return BadRequest();
            }

            _context.Entry(trangThai).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrangThaiExists(id))
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

        // POST: api/TrangThai
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrangThai>> PostTrangThai(TrangThai trangThai)
        {
          if (_context.TrangThais == null)
          {
              return Problem("Entity set 'CareCa1Context.TrangThais'  is null.");
          }
            _context.TrangThais.Add(trangThai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrangThai", new { id = trangThai.TrangThaiId }, trangThai);
        }

        // DELETE: api/TrangThai/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrangThai(int id)
        {
            if (_context.TrangThais == null)
            {
                return NotFound();
            }
            var trangThai = await _context.TrangThais.FindAsync(id);
            if (trangThai == null)
            {
                return NotFound();
            }

            _context.TrangThais.Remove(trangThai);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrangThaiExists(int id)
        {
            return (_context.TrangThais?.Any(e => e.TrangThaiId == id)).GetValueOrDefault();
        }
    }
}
