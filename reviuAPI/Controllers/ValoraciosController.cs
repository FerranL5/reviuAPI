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
    public class ValoraciosController : ControllerBase
    {
        private readonly ReviuContext _context;

        public ValoraciosController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/Valoracios
        [Route("api/Valoracios")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valoracio>>> GetValoracios()
        {
            return await _context.Valoracios.ToListAsync();
        }

        // GET: api/Valoracios/5
        [Route("api/Valoracios/{id}")]
        [HttpGet]
        public async Task<ActionResult<Valoracio>> GetValoracio(int id)
        {
            var valoracio = await _context.Valoracios.FindAsync(id);

            if (valoracio == null)
            {
                return NotFound();
            }

            return valoracio;
        }

        // PUT: api/Valoracios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Valoracios/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutValoracio(int id, Valoracio valoracio)
        {
            if (id != valoracio.ValoracioId)
            {
                return BadRequest();
            }

            _context.Entry(valoracio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoracioExists(id))
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

        // POST: api/Valoracios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Valoracios")]
        [HttpPost]
        public async Task<ActionResult<Valoracio>> PostValoracio(Valoracio valoracio)
        {
            _context.Valoracios.Add(valoracio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValoracio", new { id = valoracio.ValoracioId }, valoracio);
        }

        // DELETE: api/Valoracios/5
        [Route("api/Valoracios/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteValoracio(int id)
        {
            var valoracio = await _context.Valoracios.FindAsync(id);
            if (valoracio == null)
            {
                return NotFound();
            }

            _context.Valoracios.Remove(valoracio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValoracioExists(int id)
        {
            return _context.Valoracios.Any(e => e.ValoracioId == id);
        }
    }
}
