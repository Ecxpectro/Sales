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
            return Ok(await _context.Countries.Include(x => x.States).ToListAsync());
        }
        [HttpGet("full")]
        public async Task<IActionResult> GetFullAsync()
        {
            return Ok(await _context.Countries
                .Include(x => x.States!)
                .ThenInclude(x => x.Cities)
                .ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var coutry = await _context.Countries
                .Include(x => x.States!)
                .ThenInclude (x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (coutry == null)
            {
                return NotFound();
            }

            return Ok(coutry);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            try
            {
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbupdateException)
            {
                if (dbupdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("There's a country with the same name.");
                }

                return BadRequest(dbupdateException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PutAsync(Country country)
        {
            try
            {
                _context.Countries.Update(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbupdateException)
            {
                if (dbupdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("There's a country with the same name.");
                }

                return BadRequest(dbupdateException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
