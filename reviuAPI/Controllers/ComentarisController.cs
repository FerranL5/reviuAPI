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
    public class ComentarisController : ControllerBase
    {
        private readonly ReviuContext _context;

        public ComentarisController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/Comentaris
        [Route("api/Comentaris")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentari>>> GetComentaris()
        {
            return await _context.Comentaris.ToListAsync();
        }

        // GET: api/Comentaris/5
        [Route("api/Comentaris/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Comentari>>> GetComentari(int id)
        {
            var comentari = _context.Comentaris.Where(x=> x.EsResposta == id).ToList();

            if (comentari == null)
            {
                return NotFound();
            }

            return comentari;
        }

        // GET: api/Comentaris/v/5
        [Route("api/Comentaris/{type}/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Comentari>>> GetComentari(int id, char type)
        {
            List<Comentari> comentari = null;
            if (type == 'v')
            {
                comentari = _context.Comentaris.Where(x => x.FkValoracioId == id).ToList();

            } else if (type == 'c')
            {
                comentari = _context.Comentaris.Where(x => x.FkContingutId == id).ToList();

            }
            

            if (comentari == null)
            {
                return NotFound();
            }

            return comentari;
        }

        // PUT: api/Comentaris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Comentaris/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutComentari(int id, Comentari comentari)
        {
            if (id != comentari.ComentariId)
            {
                return BadRequest();
            }

            _context.Entry(comentari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentariExists(id))
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

        // POST: api/Comentaris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Comentaris")]
        [HttpPost]
        public async Task<ActionResult<Comentari>> PostComentari(Comentari comentari)
        {
            _context.Comentaris.Add(comentari);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentari", new { id = comentari.ComentariId }, comentari);
        }

        // DELETE: api/Comentaris/5
        [Route("api/Comentaris/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComentari(int id)
        {
            var comentari = await _context.Comentaris.FindAsync(id);
            if (comentari == null)
            {
                return NotFound();
            }

            _context.Comentaris.Remove(comentari);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentariExists(int id)
        {
            return _context.Comentaris.Any(e => e.ComentariId == id);
        }
    }
}
