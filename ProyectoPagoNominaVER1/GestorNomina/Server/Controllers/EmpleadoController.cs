using GestorNomina.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestorNomina.Shared;
using Microsoft.EntityFrameworkCore;

namespace GestorNomina.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public EmpleadoController(ApplicationDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetEmpleado()
        {
            var lista = await _context.Empleados.ToListAsync();
            return Ok(lista);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Empleado>>> GetSingleEmpleado(int id)
        {
            var miobjeto = await _context.Empleados.FirstOrDefaultAsync(ob => ob.Id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }

            return Ok(miobjeto);
        }
        [HttpPost]

        public async Task<ActionResult<Empleado>> CreateEmpleado(Empleado objeto)
        {

            _context.Empleados.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbEmpleado());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Empleado>>> UpdateEmpleado(Empleado objeto)
        {

            var DbObjeto = await _context.Empleados.FindAsync(objeto.Id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.Nombre = objeto.Nombre;
           


            await _context.SaveChangesAsync();

            return Ok(await _context.Empleados.ToListAsync());


        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<Empleado>>> DeleteEmpleado(int id)
        {
            var DbObjeto = await _context.Empleados.FirstOrDefaultAsync(Ob => Ob.Id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }

            _context.Empleados.Remove(DbObjeto);
            await _context.SaveChangesAsync();

            return Ok(await GetDbEmpleado());
        }


        private async Task<List<Empleado>> GetDbEmpleado()
        {
            return await _context.Empleados.ToListAsync();
        }
    }
}

