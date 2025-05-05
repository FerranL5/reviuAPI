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
    public class LlistesController : ControllerBase
    {
        private readonly ReviuContext _context;
        consumidorTMDB cTMDB = new consumidorTMDB();
        public LlistesController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/Llistes
        [Route("api/Llistes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lliste>>> GetLlistes()
        {
            return await _context.Llistes.ToListAsync();
        }

        // GET: api/Llistes/5
        [Route("api/Llistes/{id}")]
        [HttpGet]
        public async Task<ActionResult<Lliste>> GetLliste(int id)
        {
            var lliste = await _context.Llistes.FindAsync(id);

            if (lliste == null)
            {
                return NotFound();
            }

            lliste.CuntigutLlistes = _context.CuntigutLlistes.Where(x => x.FkLlistaId == id).ToList();

            return lliste;
        }

        // GET: api/LlistesUsuari/5
        [Route("api/LlistesUsuari/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Lliste>>> GetLlistesUsuari(int id)
        {
            var lliste = _context.Llistes.Where(x => x.FkUsuariId == id).ToList();

            if (lliste == null)
            {
                return NotFound();
            }

            foreach (Lliste ll in lliste)
            {
                ll.CuntigutLlistes = _context.CuntigutLlistes.Where(x => x.FkLlistaId == ll.LlistaId).ToList();
            }

            foreach (Lliste ll in lliste)
            {
                foreach (CuntigutLliste cl in ll.CuntigutLlistes)
                {
                    cl.FkContingut = _context.Continguts.Where(x => x.ContingutId == cl.FkContingutId).First();
                }
            }

            return lliste;
        }

        // PUT: api/Llistes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Llistes/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutLliste(int id, Lliste lliste)
        {
            if (id != lliste.LlistaId)
            {
                return BadRequest();
            }

            _context.Entry(lliste).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlisteExists(id))
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

        // POST: api/Llistes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Llistes")]
        [HttpPost]
        public async Task<ActionResult<Lliste>> PostLliste([FromBody] Lliste lliste)
        {
            _context.Llistes.Add(lliste);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLliste", new { id = lliste.LlistaId }, lliste);
        }

        // DELETE: api/Llistes/5
        [Route("api/Llistes/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLliste(int id)
        {
            var lliste = await _context.Llistes.FindAsync(id);
            if (lliste == null)
            {
                return NotFound();
            }

            var contingutLliste = _context.CuntigutLlistes.Where(x => x.FkLlistaId == id).ToList();
            _context.CuntigutLlistes.RemoveRange(contingutLliste);

            _context.Llistes.Remove(lliste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LlisteExists(int id)
        {
            return _context.Llistes.Any(e => e.LlistaId == id);
        }
    }
}
