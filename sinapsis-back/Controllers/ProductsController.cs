using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sinapsis_back.Context;
using sinapsis_back.Models;
using sinapsis_back.Dto;

namespace sinapsis_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            if (products == null)
                return BadRequest();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var entity = _context.Products.Add(product).Entity;
            await _context.SaveChangesAsync();
            if (entity == null)
                return BadRequest();
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchDTO searchDTO)
        {
            List<Product> products = await _context.Products
                .Where(p => 
                    p.Name.Contains(searchDTO.Name) || 
                    p.IdCategory == searchDTO.CategoryId ||
                    p.IdMark == searchDTO.TypeId)
                .ToListAsync();
            if (products == null)
                return BadRequest();
            return Ok(products);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            if (ProductExists(product.Id))
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if(product != null) {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
