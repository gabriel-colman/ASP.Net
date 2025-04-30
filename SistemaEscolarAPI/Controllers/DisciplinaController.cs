using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;
using SistemaEscolarAPI.Bd;
using Microsoft.EntityFrameworkCore;


namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController
    {
        priate readonly AppDbContext _context; // Injeção de dependência do contexto do banco de dados

        public DisciplinaController(AppDbContext context) // Contrutor que recebe o contexto do banco
        {
            _context = context; // Inicializa o contexto do banco de dados
        }

        [HttpGet] // Método para obter todas as disciplinas
        public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> Get()
        // async para deixar a opreação assíncrona e não bloquear o thread
        // Task<ActionResult<IEnumerable<DisciplinaDTO>>> para retornar uma lista de DTOs de disciplinas
        // IEnumerable<DisciplinaDTO> é uma interface que representa uma coleção de objetos do tipo DisciplinaDTO
        {
            var disciplinas = await _context.Disciplinas
                .Include(d => d.Curso) // Inclui a entidade Curso relacionada
                .Select(disciplinas => new DisciplinaDTO { Descricao = disciplinas.Descricao, Curso = disciplinas.Curso.Descricao }) // Seleciona as disciplinas e projeta em um DTO
                .ToListAsync(); // Converte para uma lista assíncrona

            return Ok(disciplinas); // Retorna a lista de disciplinas com status 200 OK
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaDTO disciplinaDTO)
        {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado");

            var disciplina = new Disciplina { Descricao = disciplinaDTO.Descricao, CursoId = Curso.ID };
            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaDTO disciplinaDTO)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id); //  Vai fazer a procura do aluno, ou seja da enntidade pelo seu indificador
            if (disciplina == null) return NotFound("Disciplina não encontada");// Se caso si caso cair nesse condição  404
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encotrado"); //  Se caso caso cair nesse condição da erro 400

            disciplina.Descricao = disciplinaDTO.Descricao; // Aqui vai atualizar o ALuno no models e no DTO
            _context.Disciplinas.Update(disciplina); // Atualiza a disciplina no contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna status 204 No Content

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id); // Procura a disciplina pelo ID
            if (disciplina == null) return NotFound("Disciplina não encontrada"); // Se não encontrar, retorna 404

            _context.Disciplinas.Remove(disciplina); // Remove a disciplina do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            return NoContent(); // Retorna status 204 No Content
        }
    }