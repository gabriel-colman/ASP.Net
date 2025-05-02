
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTOs;
using SistemaEscolarAPI.DB;

namespace SistemaEscolarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisciplinaAlunoCursoController : ControllerBase
{
    private readonly AppDbContext _context; // Injeção de dependência do contexto do banco de dados

    public DisciplinaAlunoCursoController(AppDbContext context) // Contrutor que recebe o contexto do banco
    {
        _context = context; // Inicializa o contexto do banco de dados
    }

    [HttpGet] // Método para obter todas as disciplinas
    public async Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> Get()
    // async para deixar a opreação assíncrona e não bloquear o thread
    // Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> para retornar uma lista de DTOs de disciplinas
    // IEnumerable<DisciplinaAlunoCursoDTO> é uma interface que representa uma coleção de objetos do tipo DisciplinaAlunoCursoDTO
    // ActionResult é uma classe base para resultados de ação em controladores ASP.NET
    {
        var regitros = await _context.DisciplinaAlunoCurso
            .Include(d => d.Aluno) // o include é usado para incluir entidades relaciondas na consulta
            .Include(d => d.Curso)
            .Include(d => d.Disciplina)
        
          .Select(d => new DisciplinaAlunoCursoDTO
          {
              Id = d.AlunoId + d.CursoId + d.DisciplinaId,
              AlunoId = d.AlunoId,
              AlunoNome = d.Aluno.Nome,
              CursoId = d.CursoId,
              CursoDescricao = d.Curso.Descricao,
              DisciplinaId = d.DisciplinaId,
              DisciplinaDescricao = d.Disciplina.Descricao
          })
          .ToListAsync(); // Converte para uma lista assíncrona


        return Ok(regitros); // Retorna a lista de disciplinas com status 200 OK
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
    {
        var entidade = new DisciplinaAlunoCurso
        {
            AlunoId = disciplinaAlunoCursoDTO.AlunoId,
            CursoId = disciplinaAlunoCursoDTO.CursoId,
            DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId
        };
        _context.DisciplinaAlunoCurso.Add(entidade);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
    {
        var entidade = await _context.DisciplinaAlunoCurso.FindAsync(id);
        if (entidade == null)
        {
            return NotFound();
        }

        entidade.AlunoId = disciplinaAlunoCursoDTO.AlunoId;
        entidade.CursoId = disciplinaAlunoCursoDTO.CursoId;
        entidade.DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId;

        _context.DisciplinaAlunoCurso.Update(entidade);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entidade = await _context.DisciplinaAlunoCurso.FindAsync(id);
        if (entidade == null)
        {
            return NotFound();
        }

        _context.DisciplinaAlunoCurso.Remove(entidade);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DisciplinaAlunoCursoDTO>> GetById(int id)
    {
        var relacoes = await _context.DisciplinaAlunoCurso
            .Include(d => d.Aluno)
            .Include(d => d.Curso)
            .Include(d => d.Disciplina)
            .ToListAsync();

        var relacao = relacoes.FirstOrDefault(r => r.AlunoId + r.CursoId + r.DisciplinaId == id);
        if (relacao == null)
            return NotFound("Relação não encontrada.");

        var dto = new DisciplinaAlunoCursoDTO
        {
            Id = relacao.AlunoId + relacao.CursoId + relacao.DisciplinaId,
            AlunoId = relacao.AlunoId,
            AlunoNome = relacao.Aluno.Nome,
            CursoId = relacao.CursoId,
            CursoDescricao = relacao.Curso.Descricao,
            DisciplinaId = relacao.DisciplinaId,
            DisciplinaDescricao = relacao.Disciplina.Descricao
        };

        return Ok(dto);
            
    }

}
