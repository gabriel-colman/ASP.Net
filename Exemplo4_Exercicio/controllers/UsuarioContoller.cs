using Microsoft.AspNetCore.Mvc;
using Exemplo5ComBancoEntity.Models;
using Exemplo5ComBancoEntity.database;
using Microsoft.EntityFrameworkCore;

namespace Exemplo5ComBancoEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post([FromBody] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Put(int id, [FromBody] Usuario usuario)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Password = usuario.Password;
            existente.Nome = usuario.Nome;
            existente.Ramal = usuario.Ramal;
            existente.Especialidade = usuario.Especialidade;

            await _context.SaveChangesAsync();
            return existente;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Usuarios.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


// Segue o cascade para quando eu deletar um usuário, deletar as máquinas e softwares associados a ele:

// ALTER TABLE maquina
// DROP CONSTRAINT maquina_fk_usuario_fkey;

// ALTER TABLE maquina
// ADD CONSTRAINT maquina_fk_usuario_fkey
// FOREIGN KEY (fk_usuario) REFERENCES usuarios(id_usuario)
// ON DELETE CASCADE;


// ALTER TABLE software
// DROP CONSTRAINT software_fk_maquina_fkey;

// ALTER TABLE software
// ADD CONSTRAINT software_fk_maquina_fkey
// FOREIGN KEY (fk_maquina) REFERENCES maquina(id_maquina)
// ON DELETE CASCADE;
