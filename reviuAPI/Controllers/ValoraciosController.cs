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
            List<Valoracio> valoracions = new List<Valoracio>();
            valoracions = _context.Valoracios.ToList();

            foreach(Valoracio v in valoracions)
            {
                v.FkUsuari = _context.Usuaris.Where(x => x.UsuariId == v.FkUsuariId).First();
                v.FkContingut = _context.Continguts.Where(x => x.ContingutId == v.FkContingutId).First();
            }

            return valoracions;
        }

        // GET: api/Valoracios/5
        [Route("api/Valoracios/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Valoracio>>> GetValoracios(int id)
        {
            var valoracions = _context.Valoracios.Where(x=>x.FkUsuariId == id).ToList();
            
            if (valoracions == null)
            {
                return NotFound();
            }

            foreach (Valoracio v in valoracions)
            {
                v.FkContingut = _context.Continguts.Where(x => x.ContingutId == v.FkContingutId).First();
            }

            return valoracions;
        }

        // GET: api/ValoraciosContingut/5
        [Route("api/ValoraciosContingut/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Valoracio>>> GetValoraciosContingut(int id)
        {
            var valoracions = _context.Valoracios.Where(x => x.FkContingutId == id).ToList();

            if (valoracions == null)
            {
                return NotFound();
            }

            foreach (Valoracio v in valoracions)
            {
                v.FkContingut = _context.Continguts.Where(x => x.ContingutId == v.FkContingutId).First();
            }

            return valoracions;
        }

        [Route("api/Valoracios/{id}/{data}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valoracio>>?> GetValoracioNoves(int id, string data)
        {
            var seguits = _context.Seguiments.Where(x => x.Segueix == id).ToList();

            var valoracions = new List<Valoracio>();

            var dataFormatada = DateTime.Parse(data);

            foreach (Seguiment s in seguits)
            {
                valoracions.AddRange(_context.Valoracios.Where(x => x.FkUsuariId == s.EsSeguit && x.DataPublicacioValoracio > dataFormatada).ToList());
            }

            if (valoracions.Count <= 0)
            {
                return null;
            }

            foreach (Valoracio v in valoracions)
            {
                v.FkUsuari = _context.Usuaris.Where(x => x.UsuariId == v.FkUsuariId).First();
                v.FkContingut = _context.Continguts.Where(x => x.ContingutId == v.FkContingutId).First();
            }

            return valoracions.OrderByDescending(x=>x.DataPublicacioValoracio).ToList();
        }

        // PUT: api/Valoracios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Valoracios/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutValoracio(int id, [FromBody]Valoracio valoracio)
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
        public async Task<ActionResult<Valoracio>> PostValoracio([FromBody]Valoracio valoracio)
        {
            var valo = _context.Valoracios.Where(x => x.FkUsuariId == valoracio.FkUsuariId && x.FkContingutId == valoracio.FkContingutId).FirstOrDefault();
            if(valo == null)
            {
                _context.Valoracios.Add(valoracio);
            } else
            {
                valo.Puntuacio = valoracio.Puntuacio;
            }
            

            _context.SaveChanges();

            var sumaValoracions = _context.Valoracios.Where(x => x.FkContingutId == valoracio.FkContingutId).Select(x => x.Puntuacio).Sum();
            var numeroValoracions = _context.Valoracios.Where(x => x.FkContingutId == valoracio.FkContingutId).Count();
            float puntuacio = (float)sumaValoracions / numeroValoracions;

            var contingut = _context.Continguts.Where(x => x.ContingutId == valoracio.FkContingutId).First();
                contingut.Valoracio = puntuacio;

            _context.SaveChanges();

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
