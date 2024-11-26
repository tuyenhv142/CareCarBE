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
    public class DichVuController : ControllerBase
    {
        private readonly CareCa1Context _context;

        public DichVuController(CareCa1Context context)
        {
            _context = context;
        }

        // GET: api/DichVu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DichVu>>> GetDichVus()
        {
          if (_context.DichVus == null)
          {
              return NotFound();
          }
            return await _context.DichVus.ToListAsync();
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVu>> GetDichVu(int id)
        {
          if (_context.DichVus == null)
          {
              return NotFound();
          }
            var dichVu = await _context.DichVus.FindAsync(id);

            if (dichVu == null)
            {
                return NotFound();
            }

            return dichVu;
        }

        // PUT: api/DichVu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDichVu(int id, DichVu dichVu)
        {
            if (id != dichVu.DichVuId)
            {
                return BadRequest();
            }

            _context.Entry(dichVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DichVuExists(id))
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

        // POST: api/DichVu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DichVu>> PostDichVu(DichVu dichVu)
        {
          if (_context.DichVus == null)
          {
              return Problem("Entity set 'CareCa1Context.DichVus'  is null.");
          }
            _context.DichVus.Add(dichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDichVu", new { id = dichVu.DichVuId }, dichVu);
        }

        // DELETE: api/DichVu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDichVu(int id)
        {
            if (_context.DichVus == null)
            {
                return NotFound();
            }
            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }

            _context.DichVus.Remove(dichVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DichVuExists(int id)
        {
            return (_context.DichVus?.Any(e => e.DichVuId == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<List<DichVu>>> search(string searchTerm)
        {
            try
            {
                if (_context.DichVus == null)
                {
                    return NotFound();
                }

                var results = await _context.DichVus
                    .Where(kh => kh.TenDichVu.Contains(searchTerm))
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

        [HttpPost]
        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("Không có hình ảnh được tải lên");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{formFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var imageUrl = $"/src/assets/{fileName}";

            return Ok(new { url = imageUrl });
        }

        //[HttpGet]
        //[Route("GetImageByPath")]
        //public IActionResult GetImageByPath(string path)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(path))
        //            return BadRequest("Invalid path.");

        //        if (!System.IO.File.Exists(path))
        //            return NotFound("File not found.");

        //        var fileBytes = System.IO.File.ReadAllBytes(path);
        //        return File(fileBytes, "image/jpeg");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}
