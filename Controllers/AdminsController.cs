﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrimeAdminAPI.Database;
using CrimeAdminAPI.Models;
using Microsoft.AspNetCore.Cors;
using NuGet.Protocol.Plugins;

namespace CrimeAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("corsPolicy")]
    public class AdminsController : ControllerBase
    {
        private readonly CrimeDbContext _context;

        public AdminsController(CrimeDbContext context)
        {
            _context = context;
        }

        // GET: api/
        [EnableCors("corsPolicy")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
          if (_context.Admin == null)
          {
              return NotFound();
          }
            return await _context.Admin.ToListAsync();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
          if (_context.Admin == null)
          {
              return NotFound();
          }
            var admin = await _context.Admin.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
          if (_context.Admin == null)
          {
              return Problem("Entity set 'CrimeDbContext.Admin'  is null.");
          }
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (_context.Admin == null)
            {
                return NotFound();
            }
            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return (_context.Admin?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }

        // POST: api/Admins/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (_context.Admin == null)
            {
                return Problem("Entity set 'CrimeDbContext.Admin' is null.");
            }

            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.AdminName== loginRequest.Username && a.AdminPassword == loginRequest.Password);

            if (admin == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            return Ok(new { message = "Login successful." });
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
