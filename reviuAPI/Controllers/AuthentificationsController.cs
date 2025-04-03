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
    public class AuthentificationsController : ControllerBase
    {
        private readonly ReviuContext _context;

        public AuthentificationsController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/Authentifications
        [Route("api/Authentifications")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentification>>> GetAuthentifications()
        {
            return await _context.Authentifications.ToListAsync();
        }

        // GET: api/Authentifications/5
        [Route("api/Authentifications/{id}")]
        [HttpGet]
        public async Task<ActionResult<Authentification>> GetAuthentification(int id)
        {
            var authentification = await _context.Authentifications.FindAsync(id);

            if (authentification == null)
            {
                return NotFound();
            }

            return authentification;
        }

        // PUT: api/Authentifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Authentifications/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutAuthentification(int id, Authentification authentification)
        {
            if (id != authentification.AuthentificationId)
            {
                return BadRequest();
            }

            _context.Entry(authentification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthentificationExists(id))
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

        // POST: api/Authentifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Authentifications")]
        [HttpPost]
        public async Task<ActionResult<Authentification>> PostAuthentification(Authentification authentification)
        {
            Usuari usuari = new Usuari();
            usuari.NomUsuari = authentification.FkUsari.NomUsuari;
            _context.Usuaris.Add(usuari);
            _context.SaveChanges();

            authentification.FkUsariId = usuari.UsuariId;
            authentification.FkUsari = usuari;
            _context.Authentifications.Add(authentification);
            await _context.SaveChangesAsync();

            Lliste ll1 = new Lliste();
            ll1.NomLlista = "Series a veure";
            ll1.FotoLlista = null;
            ll1.EsPublica = true;
            ll1.FkUsuariId = usuari.UsuariId;

            Lliste ll2 = new Lliste();
            ll2.NomLlista = "Series vistes";
            ll2.FotoLlista = null;
            ll2.EsPublica = true;
            ll2.FkUsuariId = usuari.UsuariId;

            _context.Llistes.Add(ll1);
            _context.Llistes.Add(ll2);

            _context.SaveChanges();

            return CreatedAtAction("GetAuthentification", new { id = authentification.AuthentificationId }, authentification);
        }

        // DELETE: api/Authentifications/5
        [Route("api/Authentifications/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthentification(int id)
        {
            var authentification = await _context.Authentifications.FindAsync(id);
            if (authentification == null)
            {
                return NotFound();
            }

            _context.Authentifications.Remove(authentification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthentificationExists(int id)
        {
            return _context.Authentifications.Any(e => e.AuthentificationId == id);
        }
    }
}
