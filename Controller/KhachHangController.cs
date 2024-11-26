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
    public class KhachHangController : ControllerBase
    {
        private readonly CareCa1Context _context;

        public KhachHangController(CareCa1Context context)
        {
            _context = context;
        }

        // GET: api/KhachHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHangs()
        {
          if (_context.KhachHangs == null)
          {
              return NotFound();
          }
            return await _context.KhachHangs.ToListAsync();
        }

        // GET: api/KhachHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
          if (_context.KhachHangs == null)
          {
              return NotFound();
          }
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return khachHang;
        }

        // PUT: api/KhachHang/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.KhachHangId)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHang
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
          if (_context.KhachHangs == null)
          {
              return Problem("Entity set 'CareCa1Context.KhachHangs'  is null.");
          }
            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKhachHang", new { id = khachHang.KhachHangId }, khachHang);
        }

        // DELETE: api/KhachHang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(int id)
        {
            return (_context.KhachHangs?.Any(e => e.KhachHangId == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<KhachHang>>> search(string searchTerm)
        {
            try
            {
                if (_context.KhachHangs == null)
                {
                    return NotFound();
                }

                // Thực hiện truy vấn tìm kiếm khách hàng theo tên
                var results = await _context.KhachHangs
                    .Where(kh => kh.TenKhachHang.Contains(searchTerm))
                    .ToListAsync();

                if (results.Count == 0)
                {
                    return NotFound();
                }

                return results;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi nếu cần thiết
                return StatusCode(500, ex.Message);
            }
        }
    }
}
