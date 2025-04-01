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
    public class ContingutsController : ControllerBase
    {
        private readonly ReviuContext _context;

        consumidorTMDB cTMDB = new consumidorTMDB();

        public ContingutsController(ReviuContext context)
        {
            _context = context;
        }

        // GET: api/Continguts
        [Route("api/Continguts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contingut>>> GetContinguts()
        {
            return await _context.Continguts.ToListAsync();
        }

        // GET: api/Continguts/5
        [Route("api/Continguts/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Contingut>> GetContingut(int id)
        {
            var contingut = await _context.Continguts.FindAsync(id);

            if (contingut == null)
            {
                return NotFound();
            }

            return contingut;
        }

        // GET: api/Continguts/title
        [Route("api/Continguts/{title}")]
        [HttpGet]
        public async Task<ActionResult<buscarContingutPerNom>> GetContingut(string title)
        {
            var contingut = cTMDB.buscarContingutPerNom(title);

            if (contingut == null)
            {
                return NotFound();
            }

            return contingut;
        }

        // GET: api/ContingutsDTO/tv/2
        [Route("api/ContingutsDTO/{type}/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<ContingutDTO>> GetContingutDTO(int id, string type)
        {
            var contingut = cTMDB.GetContingutDTO(id, type);

            var existeix = _context.Continguts.Where(x => x.TmdbId == id && x.tipus == type).FirstOrDefault();


            if (contingut == null)
            {
                return NotFound();
            } else
            {
                if(existeix == null)
                {
                    Contingut con = new Contingut();
                    con.TmdbId = id;
                    con.Valoracio = 0;
                    con.tipus = type;

                    _context.Continguts.Add(con);
                    _context.SaveChanges();
                }

                //if (contingut.seasons != null)
                //{
                //    for (int i = 0; i < contingut.seasons.Count; i++)
                //    {

                //        contingut.seasons[i] = cTMDB.GetSeasonDeatails(id, i + 1);

                //    }
                //}
            }

            return contingut;
        }

        // GET: api/recomendation/movie/6
        [Route("api/recomendation/{type}/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<resultatsRecomanacions>> GetRecomendation(int id, string type)
        {
            var contingut = cTMDB.GetRecomanacions(id, type);


            if (contingut == null)
            {
                return NotFound();
            }
            
            return contingut;
        }

        [Route("api/Season/{id:int}/{season:int}")]
        [HttpGet]
        public async Task<ActionResult<season>> GetSeasonDetails(int id, int season)
        {
            var contingut = cTMDB.GetSeasonDeatails(id, season);

            if (contingut == null)
            {
                return NotFound();
            }

            return contingut;
        }

        [Route("api/ultimsLlancaments/")]
        [HttpGet]
        public async Task<ActionResult<resultatsLlancaments>> GetUltimsLlancaments()
        {
            var contingut = cTMDB.GetUltimsLlancaments();

            if (contingut == null)
            {
                return NotFound();
            }

            return contingut;
        }

        // PUT: api/Continguts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Continguts/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutContingut(int id, Contingut contingut)
        {
            if (id != contingut.ContingutId)
            {
                return BadRequest();
            }

            _context.Entry(contingut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContingutExists(id))
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

        // POST: api/Continguts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Continguts")]
        [HttpPost]
        public async Task<ActionResult<Contingut>> PostContingut(Contingut contingut)
        {
            _context.Continguts.Add(contingut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContingut", new { id = contingut.ContingutId }, contingut);
        }

        // DELETE: api/Continguts/5
        [Route("api/Continguts/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteContingut(int id)
        {
            var contingut = await _context.Continguts.FindAsync(id);
            if (contingut == null)
            {
                return NotFound();
            }

            _context.Continguts.Remove(contingut);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContingutExists(int id)
        {
            return _context.Continguts.Any(e => e.ContingutId == id);
        }
    }
}
