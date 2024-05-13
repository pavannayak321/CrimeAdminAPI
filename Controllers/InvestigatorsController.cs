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
    public class InvestigatorsController : ControllerBase
    {
        private readonly CrimeDbContext _context;

        public InvestigatorsController(CrimeDbContext context)
        {
            _context = context;
        }

        // GET: api/Investigators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investigator>>> GetInvestigator()
        {
          if (_context.Investigator == null)
          {
              return NotFound();
          }
            return await _context.Investigator.ToListAsync();
        }

        // GET: api/Investigators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investigator>> GetInvestigator(int id)
        {
          if (_context.Investigator == null)
          {
              return NotFound();
          }
            var investigator = await _context.Investigator.FindAsync(id);

            if (investigator == null)
            {
                return NotFound();
            }

            return investigator;
        }

        // PUT: api/Investigators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigator(int id, Investigator investigator)
        {
            if (id != investigator.InvestigatorId)
            {
                return BadRequest();
            }

            _context.Entry(investigator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigatorExists(id))
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

        // POST: api/Investigators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investigator>> PostInvestigator(Investigator investigator)
        {
          if (_context.Investigator == null)
          {
              return Problem("Entity set 'CrimeDbContext.Investigator'  is null.");
          }
            _context.Investigator.Add(investigator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvestigator", new { id = investigator.InvestigatorId }, investigator);
        }

        // DELETE: api/Investigators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigator(int id)
        {
            if (_context.Investigator == null)
            {
                return NotFound();
            }
            var investigator = await _context.Investigator.FindAsync(id);
            if (investigator == null)
            {
                return NotFound();
            }

            _context.Investigator.Remove(investigator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestigatorExists(int id)
        {
            return (_context.Investigator?.Any(e => e.InvestigatorId == id)).GetValueOrDefault();
        }
    }
}
