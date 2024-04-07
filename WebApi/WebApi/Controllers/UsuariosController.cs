using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApi.Data;
using MiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                // Log the exception message or return a more specific error response
                return StatusCode(500, "Internal server error");
            }

        }

       // GET: api/Usuarios
[HttpGet]
public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
{
    return await _context.Usuarios.ToListAsync();
}
    }
}
