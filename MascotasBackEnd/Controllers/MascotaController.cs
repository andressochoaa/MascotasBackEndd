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

        //peticion get
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
    }
}
