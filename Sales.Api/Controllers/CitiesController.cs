using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Api.Data;
using Sales.Api.Helpers;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("/api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public CitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }


            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(City city)
        {
            try
            {
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException dbupdateException)
            {
                if (dbupdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("There's a city with the same name.");
                }

                return BadRequest(dbupdateException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PutAsync(City city)
        {
            try
            {
                _context.Cities.Update(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException dbupdateException)
            {
                if (dbupdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("There's a city with the same name.");
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
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
