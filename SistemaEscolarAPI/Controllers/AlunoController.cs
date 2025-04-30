using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTO;
using Microsoft.AspNetCore.Mvc;


namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context; // Injeção de dependência do contexto do banco de dados

        public AlunoController(AppDbContext context) // Contrutor que recebe o contexto do banco
        {
            _context = context; // Inicializa o contexto do banco de dados
        }

        [HttpGet] // Método para obter todos os alunos
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
        // async para deixar a opreação assíncrona e não bloquear o thread
        // Task<ActionResult<IEnumerable<AlunoDTO>>> para retornar uma lista de DTOs de alunos
        // IEnumerable<AlunoDTO> é uma interface que representa uma coleção de objetos do tipo AlunoDTO
        // ActionResult é uma classe base para resultados de ação em controladores ASP.NET
        {
            var alunos = await _context.Alunos
                .Include(a => a.Curso) // Inclui a entidade Turma relacionada
                .Select(alunos => new AlunoDTO) { Nome = alunos.Nome, Curso = alunos.Curso.Descricao } // Seleciona os alunos e projeta em um DTO
                .ToListAsync(); // Converte para uma lista assíncrona

            return Ok(alunos); // Retorna a lista de alunos com status 200 OK
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlunoDTO alunoDTO)
        {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encontrado");

            var aluno = new Aluno { Nome = alunoDTO.Nome, CursoId = Curso.ID };
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlunoDTO alunoDTO)
        {
            var aluno = await _context.Alunos.FindAsync(id); //  Vai fazer a procura do aluno, ou seja da enntidade pelo seu indificador
            if (aluno == null) return NotFound("Aluno não encontado");// Se caso si caso cair nesse condição  404
            var Curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);
            if (Curso == null) return BadRequest("Curso não encotrado"); //  Se caso caso cair nesse condição da erro 400

            aluno.Nome = alunoDTO.Nome; // Aqui vai atualizar o ALuno no models e no DTO

            aluno.CursoId = Curso.ID; // Atualia o Id do curso do aluno com ID do curso encotrado

            _context.Alunos.Update(aluno); //Update é metodo que atualiza a entidade no banco 
            await  _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return BadRequest("Curso não encotrado"); //  Se caso caso cair nesse condição da erro 400

            _context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();
            
            return Ok();

        }

    }
}