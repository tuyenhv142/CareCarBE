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
    public class NhanVienController : ControllerBase
    {
        private readonly CareCa1Context _context;

        public NhanVienController(CareCa1Context context)
        {
            _context = context;
        }

        // GET: api/NhanVien
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanViens()
        {
          if (_context.NhanViens == null)
          {
              return NotFound();
          }
            return await _context.NhanViens.ToListAsync();
        }

        // GET: api/NhanVien/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanVien>> GetNhanVien(int id)
        {
          if (_context.NhanViens == null)
          {
              return NotFound();
          }
            var nhanVien = await _context.NhanViens.FindAsync(id);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return nhanVien;
        }

        // PUT: api/NhanVien/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanVien(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.NhanVienId)
            {
                return BadRequest();
            }

            _context.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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

        // POST: api/NhanVien
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NhanVien>> PostNhanVien(NhanVien nhanVien)
        {
          if (_context.NhanViens == null)
          {
              return Problem("Entity set 'CareCa1Context.NhanViens'  is null.");
          }
            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNhanVien", new { id = nhanVien.NhanVienId }, nhanVien);
        }

        // DELETE: api/NhanVien/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            if (_context.NhanViens == null)
            {
                return NotFound();
            }
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NhanVienExists(int id)
        {
            return (_context.NhanViens?.Any(e => e.NhanVienId == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<NhanVien>>> search(string searchTerm)
        {
            try
            {
                if (_context.NhanViens == null)
                {
                    return NotFound();
                }

                // Thực hiện truy vấn tìm kiếm khách hàng theo tên
                var results = await _context.NhanViens
                    .Where(kh => kh.HoTen.Contains(searchTerm))
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
        [HttpGet("GetByName/{userName}")]
        public async Task<IActionResult> GetByName(string userName)
        {
            var account = await _context.NhanViens.FirstOrDefaultAsync(a => a.TenDangNhap == userName);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    }
}
