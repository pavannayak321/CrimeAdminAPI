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
    public class SuspectsController : ControllerBase
    {   
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerNameForImage = "suspectimages"; // Replace with your container name

        private readonly CrimeDbContext _context;

        public SuspectsController(CrimeDbContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;

        }

        // GET: api/Suspects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suspect>>> GetSuspect()
        {
          if (_context.Suspect == null)
          {
              return NotFound();
          }
            return await _context.Suspect.ToListAsync();
        }

        // GET: api/Suspects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suspect>> GetSuspect(int id)
        {
          if (_context.Suspect == null)
          {
              return NotFound();
          }
            var suspect = await _context.Suspect.FindAsync(id);

            if (suspect == null)
            {
                return NotFound();
            }

            return suspect;
        }

        // PUT: api/Suspects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuspect(int id, Suspect suspect)
        {
            if (id != suspect.SuspectID)
            {
                return BadRequest();
            }

            _context.Entry(suspect).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuspectExists(id))
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

        // POST: api/Suspects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suspect>> PostSuspect([FromForm] Suspect suspect, IFormFile imageFile)
        {
          if (_context.Suspect == null)
          {
              return Problem("Entity set 'CrimeDbContext.Suspect'  is null.");
          }

          if (imageFile != null && imageFile.Length > 0)
          {
            string imageUrl = await UploadFileToBlobStorageImage(imageFile);
                suspect.SuspectImageUrl = imageUrl;
          }


            _context.Suspect.Add(suspect);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuspect", new { id = suspect.SuspectID }, suspect);
        }

        // DELETE: api/Suspects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuspect(int id)
        {
            if (_context.Suspect == null)
            {
                return NotFound();
            }
            var suspect = await _context.Suspect.FindAsync(id);
            if (suspect == null)
            {
                return NotFound();
            }

            _context.Suspect.Remove(suspect);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuspectExists(int id)
        {
            return (_context.Suspect?.Any(e => e.SuspectID == id)).GetValueOrDefault();
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
