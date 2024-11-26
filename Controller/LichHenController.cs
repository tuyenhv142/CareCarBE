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
    public class LichHenController : ControllerBase
    {
        private readonly CareCa1Context _context;

        public LichHenController(CareCa1Context context)
        {
            _context = context;
        }

        // GET: api/LichHen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichHen>>> GetLichHens()
        {
          if (_context.LichHens == null)
          {
              return NotFound();
          }
            return await _context.LichHens.ToListAsync();
        }

        // GET: api/LichHen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichHen>> GetLichHen(int id)
        {
          if (_context.LichHens == null)
          {
              return NotFound();
          }
            var lichHen = await _context.LichHens.FindAsync(id);

            if (lichHen == null)
            {
                return NotFound();
            }

            return lichHen;
        }

        // PUT: api/LichHen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichHen(int id, LichHen lichHen)
        {
            if (id != lichHen.LichHenId)
            {
                return BadRequest();
            }

            _context.Entry(lichHen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichHenExists(id))
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

        // POST: api/LichHen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LichHen>> PostLichHen(LichHen lichHen)
        {
          if (_context.LichHens == null)
          {
              return Problem("Entity set 'CareCa1Context.LichHens'  is null.");
          }
            _context.LichHens.Add(lichHen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichHen", new { id = lichHen.LichHenId }, lichHen);
        }

        // DELETE: api/LichHen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichHen(int id)
        {
            if (_context.LichHens == null)
            {
                return NotFound();
            }
            var lichHen = await _context.LichHens.FindAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }

            _context.LichHens.Remove(lichHen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichHenExists(int id)
        {
            return (_context.LichHens?.Any(e => e.LichHenId == id)).GetValueOrDefault();
        }

        // GET: api/LichHen/search?searchTerm={searchTerm}
        [HttpGet("search")]
        public async Task<ActionResult<List<LichHen>>> Search(string searchTerm)
        {
            try
            {
                var lichHens = await _context.LichHens
                    .Include(lh => lh.KhachHang)
                    .Where(lh => lh.KhachHang.TenKhachHang.Contains(searchTerm))
                    .ToListAsync();

                if (lichHens.Count == 0)
                {
                    return NotFound("Không tìm thấy kết quả phù hợp.");
                }

                return lichHens;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi. Vui lòng thử lại sau.");
            }
        }
        [HttpGet("totalPaidAmount")]
        public async Task<ActionResult<decimal>> GetTotalPaidAmount()
        {
            try
            {
                decimal? totalPaidAmount = await _context.LichHens
                    .Where(lh => lh.ThanhToan == "Đã thanh toán")
                    .SumAsync(lh => lh.TongTien);

                return totalPaidAmount;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi. Vui lòng thử lại sau.");
            }
        }
        // GET: api/LichHen/ChuaThanhToan
        [HttpGet("ChuaThanhToan")]
        public async Task<ActionResult<IEnumerable<LichHen>>> GetLichHensChuaThanhToan()
        {
            try
            {
                var lichHens = await _context.LichHens
                    .Where(lh => lh.ThanhToan == "Chưa thanh toán")
                    .ToListAsync();

                return lichHens;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi. Vui lòng thử lại sau.");
            }
        }
    }
}
