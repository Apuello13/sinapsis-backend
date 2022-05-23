using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sinapsis_back.Context;
using sinapsis_back.Models;

namespace sinapsis_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Category> categories = _context.Categories.ToList();
            if(categories == null)
                return BadRequest();
            return Ok(categories);
        }
        
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> getById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            var entity = _context.Categories.Add(category).Entity;
            await _context.SaveChangesAsync();
            if (entity == null)
                return BadRequest();
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            if (CategoryExists(category.IdCategory))
            {
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.IdCategory == id)).GetValueOrDefault();
        }
    }
}
