
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
        var regitros = await _context.DisciplinasAlunosCursos
          .Select(d => new DisciplinaAlunoCursoDTO
          {
              AlunoID = d.AlunoID,
              CursoID = d.CursoID,
              DisciplinaID = d.DisciplinaID,
          })
          .ToListAsync(); // Converte para uma lista assíncrona


        return Ok(regitros); // Retorna a lista de disciplinas com status 200 OK
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
    {
        var entidade = new DisciplinaAlunoCurso
        {
            AlunoID = disciplinaAlunoCursoDTO.AlunoID,
            CursoID = disciplinaAlunoCursoDTO.CursoID,
            DisciplinaID = disciplinaAlunoCursoDTO.DisciplinaID
        };
        _context.DisciplinasAlunosCursos.Add(entidade);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
    {
        var entidade = await _context.DisciplinasAlunosCursos.FindAsync(id);
        if (entidade == null)
        {
            return NotFound();
        }

        entidade.AlunoID = disciplinaAlunoCursoDTO.AlunoID;
        entidade.CursoID = disciplinaAlunoCursoDTO.CursoID;
        entidade.DisciplinaID = disciplinaAlunoCursoDTO.DisciplinaID;

        _context.DisciplinasAlunosCursos.Update(entidade);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entidade = await _context.DisciplinasAlunosCursos.FindAsync(id);
        if (entidade == null)
        {
            return NotFound();
        }

        _context.DisciplinasAlunosCursos.Remove(entidade);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
