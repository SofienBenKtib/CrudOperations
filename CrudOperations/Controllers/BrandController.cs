using CrudOperations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext? _brandContext;

        public BrandController(BrandContext? brandContext)
        {
            _brandContext = brandContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if(_brandContext == null)
            {
                return NotFound();
            }
            return await _brandContext.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            if (_brandContext == null)
            {
                return NotFound();
            }
            var brand = await _brandContext.Brands.FindAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _brandContext.Brands.Add(brand);
            await _brandContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Brand), new { id = brand.ID }, brand);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int id, Brand brand)
        {
            if(id != brand.ID)
            {
                return BadRequest();
            }
            _brandContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _brandContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
         
        }
        private bool BrandAvailable(int id)
        {
           return (_brandContext.Brands?.Any(b => b.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(int id)
        {
            if(_brandContext == null)
            {
                return NotFound();
            }

            var brand = await _brandContext.Brands.FindAsync(id);
            if(brand == null)
            {
                return NotFound();
            }
            _brandContext.Brands.Remove(brand);
            await _brandContext.SaveChangesAsync();
            return Ok();

        }

    }
}
