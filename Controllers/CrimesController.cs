using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrimeAdminAPI.Database;
using CrimeAdminAPI.Models;

namespace CrimeAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrimesController : ControllerBase
    {
        private readonly CrimeDbContext _context;

        public CrimesController(CrimeDbContext context)
        {
            _context = context;
        }

        // GET: api/Crimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crime>>> Getcrimes()
        {
          if (_context.crimes == null)
          {
              return NotFound();
          }
            return await _context.crimes.ToListAsync();
        }

        // GET: api/Crimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crime>> GetCrime(int id)
        {
          if (_context.crimes == null)
          {
              return NotFound();
          }
            var crime = await _context.crimes.FindAsync(id);

            if (crime == null)
            {
                return NotFound();
            }

            return crime;
        }

        // PUT: api/Crimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrime(int id, Crime crime)
        {
            if (id != crime.Id)
            {
                return BadRequest();
            }

            _context.Entry(crime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrimeExists(id))
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

        // POST: api/Crimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Crime>> PostCrime(Crime crime)
        {
          if (_context.crimes == null)
          {
              return Problem("Entity set 'CrimeDbContext.crimes'  is null.");
          }
            _context.crimes.Add(crime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrime", new { id = crime.Id }, crime);
        }

        // DELETE: api/Crimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrime(int id)
        {
            if (_context.crimes == null)
            {
                return NotFound();
            }
            var crime = await _context.crimes.FindAsync(id);
            if (crime == null)
            {
                return NotFound();
            }

            _context.crimes.Remove(crime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CrimeExists(int id)
        {
            return (_context.crimes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
