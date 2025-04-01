using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reviuAPI.Models;

namespace reviuAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CuntigutLlistesController : ControllerBase
    {
        private readonly ReviuContext _context;

        public CuntigutLlistesController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/CuntigutLlistes
        [Route("api/CuntigutLlistes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuntigutLliste>>> GetCuntigutLlistes()
        {
            return await _context.CuntigutLlistes.ToListAsync();
        }

        // GET: api/CuntigutLlistes/5
        [Route("api/CuntigutLlistes/{id}")]
        [HttpGet]
        public async Task<ActionResult<CuntigutLliste>> GetCuntigutLliste(int id)
        {
            var cuntigutLliste = await _context.CuntigutLlistes.FindAsync(id);

            if (cuntigutLliste == null)
            {
                return NotFound();
            }

            return cuntigutLliste;
        }

        // PUT: api/CuntigutLlistes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/CuntigutLlistes/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutCuntigutLliste(int id, CuntigutLliste cuntigutLliste)
        {
            if (id != cuntigutLliste.ContingutLlistaId)
            {
                return BadRequest();
            }

            _context.Entry(cuntigutLliste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuntigutLlisteExists(id))
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

        // POST: api/CuntigutLlistes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/CuntigutLlistes")]
        [HttpPost]
        public async Task<ActionResult<CuntigutLliste>> PostCuntigutLliste(CuntigutLliste cuntigutLliste)
        {
            _context.CuntigutLlistes.Add(cuntigutLliste);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuntigutLliste", new { id = cuntigutLliste.ContingutLlistaId }, cuntigutLliste);
        }

        // DELETE: api/CuntigutLlistes/5
        [Route("api/CuntigutLlistes/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCuntigutLliste(int id)
        {
            var cuntigutLliste = await _context.CuntigutLlistes.FindAsync(id);
            if (cuntigutLliste == null)
            {
                return NotFound();
            }

            _context.CuntigutLlistes.Remove(cuntigutLliste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuntigutLlisteExists(int id)
        {
            return _context.CuntigutLlistes.Any(e => e.ContingutLlistaId == id);
        }
    }
}
