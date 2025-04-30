using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;
using SistemaEscolarAPI.Bd

using Microsoft.EntityFrameworkCore;


namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly AppDbContext _context; // Injeção de dependência do contexto do banco de dados

        public CursoController(AppDbContext context) // Contrutor que recebe o contexto do banco
        {
            _context = context; // Inicializa o contexto do banco de dados
        }

        [HttpGet] // Método para obter todos os cursos
        public async Task<ActionResult<IEnumerable<CursoDTO>>> Get()
        // async para deixar a opreação assíncrona e não bloquear o thread
        // Task<ActionResult<IEnumerable<CursoDTO>>> para retornar uma lista de DTOs de cursos
        // IEnumerable<CursoDTO> é uma interface que representa uma coleção de objetos do tipo CursoDTO
        // ActionResult é uma classe base para resultados de ação em controladores ASP.NET
        {
            var cursos = await _context.Cursos
                .Select(cursos => new CursoDTO { Descricao = cursos.Descricao }) // Seleciona os cursos e projeta em um DTO
                .ToListAsync(); // Converte para uma lista assíncrona

            return Ok(cursos); // Retorna a lista de cursos com status 200 OK
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CursoDTO cursoDTO)
        {
            var curso = new Curso { Descricao = cursoDTO.Descricao }; // Cria um novo objeto Curso com a descrição do DTO
            _context.Cursos.Add(curso); // Adiciona o curso ao contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados

            return Ok(); // Retorna status 200 OK
        }

        [HttpPut("{id}")]
        oublic async Task<ActionResult> Put(int id, [FromBody] CursoDTO cursoDTO)
        {
            var curso = await _context.Cursos.FindAsync(id); // Procura o curso pelo ID
            if (curso == null) return NotFound("Curso não encontrado"); // Se não encontrar, retorna 404

            curso.Descricao = cursoDTO.Descricao; // Atualiza a descrição do curso
            _context.Cursos.Update(curso); // Atualiza o curso no contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna status 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var curso = await _context.Cursos.FindAsync(id); // Procura o curso pelo ID
            if (curso == null) return NotFound("Curso não encontrado"); // Se não encontrar, retorna 404

            _context.Cursos.Remove(curso); // Remove o curso do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna status 204 No Content
        }

    }
   
}