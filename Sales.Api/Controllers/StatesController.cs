﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.Api.Data;
using Sales.Api.Helpers;
using Sales.Shared.DTOs;
using Sales.Shared.Entities;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/states")]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.States
                .Include(x => x.Cities)
                .Where(x => x.Country!.Id == pagination.Id)
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
            var queryable = _context.States
                .Where(x => x.Country!.Id == pagination.Id)
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
            var state = await _context.States
                .Include(x => x.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(State state)
        {
            try
            {
                _context.States.Add(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbupdateException)
            {
                if (dbupdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("There's a state with the same name.");
                }

                return BadRequest(dbupdateException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PutAsync(State state)
        {
            try
            {
                _context.States.Update(state);
                await _context.SaveChangesAsync();
                return Ok(state);
            }
            catch (DbUpdateException dbupdateException)
            {
                if (dbupdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("There's a state with the same name.");
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
            var state = await _context.States.FirstOrDefaultAsync(x => x.Id == id);
            if (state == null)
            {
                return NotFound();
            }
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("combo/{countryId:int}")]
        public async Task<ActionResult> GetCombo(int countryId)
        {
            return Ok(await _context.States
                .Where(x => x.CountryId == countryId)
                .ToListAsync());
        }

    }
}
