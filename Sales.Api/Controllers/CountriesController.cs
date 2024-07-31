using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Api.Data;
using Sales.Shared.Entities;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("/api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Countries.ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var coutry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (coutry == null)
            {
                return NotFound();
            }
            
            return Ok(coutry);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }
        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var coutry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (coutry == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(coutry);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
