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
    public class UsuarisController : ControllerBase
    {
        private readonly ReviuContext _context;
        private readonly IWebHostEnvironment _env;

        public UsuarisController(ReviuContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

       

        // GET: api/Usuaris
        [Route("api/Usuaris")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuaris()
        {
            return await _context.Usuaris.ToListAsync();
        }

        // GET: api/Usuaris/5
        [Route("api/Usuaris/{id:int}")]
        [HttpGet]
        public async Task<ActionResult<Usuari>> GetUsuari(int id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);
            var lliste = _context.Llistes.Where(x => x.FkUsuariId == id).ToList();

            if (usuari == null)
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

            usuari.Llistes = lliste;

            return usuari;
        }

        // GET: api/Usuaris/u
        [Route("api/Usuaris/{nom}")]
        [HttpGet]
        public async Task<ActionResult<List<Usuari>>> GetUsuari(string nom)
        {
            var usuari = _context.Usuaris.Where(x=> x.NomUsuari.Contains(nom)).ToList();

            if (usuari == null)
            {
                return NotFound();
            }         

            return usuari;
        }

        [Route("api/Usuaris/fotoUsuari/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetImageUrl(int id)
        {
            var image = await _context.Usuaris
                .Where(x => x.UsuariId == id)
                .FirstOrDefaultAsync();

            if (image == null)
                return NotFound();

            return Ok(new { imageUrl = image.FotoUsuari });
        }


        // PUT: api/Usuaris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Usuaris/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutUsuari(int id, [FromBody]Usuari usuari)
        {
            if (id != usuari.UsuariId)
            {
                return BadRequest();
            }

            _context.Entry(usuari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariExists(id))
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

        // POST: api/Usuaris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/Usuaris")]
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostUsuari(Usuari usuari)
        {   
            

            _context.Usuaris.Add(usuari);
            await _context.SaveChangesAsync();

            Usuari uCreat = _context.Usuaris.OrderBy(x=>x.UsuariId).Last();

            if (uCreat != null && uCreat.UsuariId == usuari.UsuariId)
            {

                Lliste ll1 = new Lliste();
                ll1.NomLlista = "Continguts a veure";
                ll1.FotoLlista = null;
                ll1.EsPublica = true;
                ll1.FkUsuariId = usuari.UsuariId;

                Lliste ll2 = new Lliste();
                ll2.NomLlista = "Continguts vistos";
                ll2.FotoLlista = null;
                ll2.EsPublica = true;
                ll2.FkUsuariId = usuari.UsuariId;

                _context.Llistes.Add(ll1);
                _context.Llistes.Add(ll2);


            }
            else
            {
                uCreat = _context.Usuaris.Where(x => x.NomUsuari == usuari.NomUsuari).FirstOrDefault();

                if (uCreat != null)
                {
                    Lliste ll1 = new Lliste();
                    ll1.NomLlista = "Continguts a veure";
                    ll1.FotoLlista = null;
                    ll1.EsPublica = true;
                    ll1.FkUsuariId = usuari.UsuariId;

                    Lliste ll2 = new Lliste();
                    ll2.NomLlista = "Continguts vistos";
                    ll2.FotoLlista = null;
                    ll2.EsPublica = true;
                    ll2.FkUsuariId = usuari.UsuariId;

                    _context.Llistes.Add(ll1);
                    _context.Llistes.Add(ll2);


                }
            }

                await _context.SaveChangesAsync();

                return await GetUsuari(usuari.UsuariId);
        }

        [Route("api/Usuaris/uploadImage")]
        [HttpPost]
        public async Task<ActionResult<foto>> UploadImage(IFormFile image, [FromForm] int userId)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Imagen no válida.");

            // Ruta donde guardar
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Nombre único para la imagen
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Guardar en disco
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);

            }

            // Guardar ruta en base de datos
            var imageUrl = "http://172.16.24.149:45455" + $"/uploads/{fileName}";
            var usuari = _context.Usuaris.Where(x => x.UsuariId == userId).First();
            usuari.FotoUsuari = imageUrl;
            await _context.SaveChangesAsync();

            foto f = new foto();
            f.imageUrl = imageUrl;

            return f;
        }

        // DELETE: api/Usuaris/5
        [Route("api/Usuaris/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuari(int id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari == null)
            {
                return NotFound();
            }

            _context.Usuaris.Remove(usuari);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariExists(int id)
        {
            return _context.Usuaris.Any(e => e.UsuariId == id);
        }
    }
}
