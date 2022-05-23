using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sinapsis_back.Context;
using sinapsis_back.Models;

namespace sinapsis_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarkController(AppDbContext context)
        {
            _context = context;
        }

       [HttpGet]
       public async Task<IActionResult> GetAll()
        {
            List<Mark> typeProducts = await _context.Marks.ToListAsync();
            if (typeProducts == null)
                return BadRequest();
            return Ok(typeProducts);
        }

        [HttpGet("{typeProductId}")]
        public async Task<IActionResult> GetById(int typeProductId) { 
            var typeProduct = await _context.Marks.FindAsync(typeProductId);
            if (typeProduct == null)
                return NotFound();
            return Ok(typeProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Mark typeProduct)
        {
            var entity = _context.Marks.Add(typeProduct).Entity;
            await _context.SaveChangesAsync();
            if (entity == null)
                return BadRequest();
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Mark typeProduct)
        {
            if (TypeProductExists(typeProduct.IdMark)) { 
                _context.Entry(typeProduct).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{typeProductId}")]
        public async Task<IActionResult> DeleteById(int typeProductId)
        {
            var typeProduct = await _context.Marks.FindAsync(typeProductId);
            if (typeProduct != null)
            {
                _context.Marks.Remove(typeProduct);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        private bool TypeProductExists(int id)
        {
          return (_context.Marks?.Any(e => e.IdMark == id)).GetValueOrDefault();
        }
    }
}
