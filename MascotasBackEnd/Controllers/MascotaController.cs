using AutoMapper;
using MascotasBackEnd.Models;
using MascotasBackEnd.Models.Dto;
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
        private readonly IMapper _mapper;

        //constructor mascotacontroller
        public MascotaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //peticion get lista mascotas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //todos los elementos de la tabla mascotas
                var listMascotas = await _context.Mascotas.ToListAsync();

                //implementacion dto
                var listMascotasDto = _mapper.Map<IEnumerable<MascotaDto>>(listMascotas);

                return Ok(listMascotasDto);
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

                var mascotaDto = _mapper.Map<MascotaDto>(mascota);

                return Ok(mascotaDto);
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
        public async Task<IActionResult> Post(MascotaDto mascotaDto)
        {
            try
            {
                //Mapeamos hacia mascota
                var mascota = _mapper.Map<Mascota>(mascotaDto);

                //obtenemos la fecha para el campo requerido de mascota
                mascota.FechaCreacion = DateTime.Now;
                //agregamos los datos obtenidos a mascota
                _context.Add(mascota);
                await _context.SaveChangesAsync();

                //mapeamos hacia mascotaDto
                var mascotaItemDto = _mapper.Map<MascotaDto>(mascota);


                return CreatedAtAction("Get", new { id = mascotaItemDto.Id }, mascotaItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Editamos mascota
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MascotaDto mascotaDto)
        {
            try
            {
                //mapeamos hacia mascota
                var mascota = _mapper.Map<Mascota>(mascotaDto);

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
