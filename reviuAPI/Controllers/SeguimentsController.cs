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
    public class SeguimentsController : ControllerBase
    {
        private readonly ReviuContext _context;

        public SeguimentsController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/Seguiments
        [Route("api/Seguiments")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seguiment>>> GetSeguiments()
        {
            return await _context.Seguiments.ToListAsync();
        }

        // GET: api/Seguiments/5
        [Route("api/Seguiments/{id}")]
        [HttpGet]
        public async Task<ActionResult<Seguiment>> GetSeguiment(int id)
        {
            var seguiment = await _context.Seguiments.FindAsync(id);

            if (seguiment == null)
            {
                return NotFound();
            }

            return seguiment;
        }

        // PUT: api/Seguiments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Seguiments/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutSeguiment(int id, Seguiment seguiment)
        {
            if (id != seguiment.SeguimentsId)
            {
                return BadRequest();
            }

            _context.Entry(seguiment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeguimentExists(id))
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

        // POST: api/Seguiments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Seguiments")]
        [HttpPost]
        public async Task<ActionResult<Seguiment>> PostSeguiment(Seguiment seguiment)
        {
            _context.Seguiments.Add(seguiment);

            Usuari uSegueix = new Usuari();
            Usuari uEsSeguit = new Usuari();

            uSegueix = _context.Usuaris.Where(x=> x.UsuariId == seguiment.Segueix).First();
            uEsSeguit = _context.Usuaris.Where(x => x.UsuariId == seguiment.EsSeguit).First();

            uSegueix.Seguits++;
            uEsSeguit.Seguidors++;


            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeguiment", new { id = seguiment.SeguimentsId }, seguiment);
        }

        // DELETE: api/Seguiments/5
        [Route("api/Seguiments/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSeguiment(int id)
        {
            var seguiment = await _context.Seguiments.FindAsync(id);
            if (seguiment == null)
            {
                return NotFound();
            }

            _context.Seguiments.Remove(seguiment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeguimentExists(int id)
        {
            return _context.Seguiments.Any(e => e.SeguimentsId == id);
        }
    }
}
