using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrimeAdminAPI.Database;
using CrimeAdminAPI.Models;
using Azure.Storage.Blobs;

namespace CrimeAdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CrimeDbContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerNameForImage = "userimages"; // Replace with your container name
        public UsersController(CrimeDbContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient= blobServiceClient;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromForm] User user, IFormFile imageFile)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string imageUrl = await UploadFileToBlobStorageImage(imageFile);
                user.UserImage = imageUrl;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromForm] User user, IFormFile imageFile)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'CrimeDbContext.User'  is null.");
          }

            if (imageFile != null && imageFile.Length > 0)
            {
                string imageUrl = await UploadFileToBlobStorageImage(imageFile);
                user.UserImage = imageUrl;
            }


            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        private async Task<string> UploadFileToBlobStorageImage(IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerNameForImage);
            var blobClient = blobContainerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            return blobClient.Uri.ToString();
        }
    }
}
