using MascotasBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MascotasBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        //utilizacion del appdbcontext mediante la inyeccion de dependencia
        private readonly AppDbContext _context;

        //constructor mascotacontroller
        public MascotaController(AppDbContext context)
        {
            _context = context;
        }

        //peticion get lista mascotas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //todos los elementos de la tabla mascotas
                var listMascotas = await _context.Mascotas.ToListAsync();
                return Ok(listMascotas);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        //peticion get por id de mascota
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _context.Mascotas.FindAsync(id);
                //si no existe el id, retorna el not found
                if(mascota == null)
                {
                    return NotFound();
                }
                return Ok(mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
