using CrudOperations.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminContext? _adminContext;

        public AdminController(AdminContext? adminContext)
        {
           _adminContext = adminContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            if(_adminContext == null)
            {
                return NotFound();
            }
            return await _adminContext.Admins.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            if(_adminContext == null)
            {
                return NotFound();
            }

            var admin=await _adminContext.Admins.FindAsync(id);
            if(admin == null)
            {
                return NotFound();
            }
            return admin;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostAdmin(Admin admin)
        {
            _adminContext.Admins.Add(admin);
            await _adminContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Admin), admin);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAdmin(Admin admin,int id)
        {
            if(id != admin.ID) 
            {
                return BadRequest();
            }

            _adminContext.Entry(admin).State= EntityState.Modified;

            try
            {
                await _adminContext.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException e)
            {
                await Console.Out.WriteLineAsync(e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Admin>> DeleteAdmin(int id)
        {
            if (_adminContext == null)
            {
                return NotFound();
            }

            var admin = await _adminContext.Admins.FindAsync(id);

            if(admin == null)
            {
                return NotFound(id);
            }
            _adminContext.Admins.Remove(admin);
            await _adminContext.SaveChangesAsync();
            return Ok();
        }
    }
}
