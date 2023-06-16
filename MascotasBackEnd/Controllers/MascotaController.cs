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
                if (mascota == null)
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

        // borrar mascota
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //buscamos por el id
                var mascota = await _context.Mascotas.FindAsync(id);
                if (mascota == null)
                {
                    return NotFound();
                }

                //lo removemos
                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Agregar mascota
        [HttpPost]
        public async Task<IActionResult> Post(Mascota mascota)
        {
            try
            {
                //obtenemos la fecha para el campo requerido de mascota
                mascota.FechaCreacion = DateTime.Now;
                //agregamos los datos obtenidos a mascota
                _context.Add(mascota);
                await _context.SaveChangesAsync();

                //creamos la mascota
                return CreatedAtAction("Get", new { id = mascota.Id }, mascota);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Editamos mascota
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Mascota mascota)
        {
            try
            {
                if(id != mascota.Id)
                {
                    return BadRequest();
                }

                //busca el id para editarlo
                var mascotaItem = await _context.Mascotas.FindAsync(id);

                //si no lo encuentra retorna 404
                if (mascotaItem == null)
                {
                    return NotFound();
                }

                mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Especie = mascota.Especie;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.FechaNacimiento = mascota.FechaNacimiento;
                mascotaItem.IdDueno = mascota.IdDueno;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
