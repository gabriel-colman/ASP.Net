using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTOs;
using SistemaEscolarAPI.DB;

namespace SistemaEscolarAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DisciplinaController : ControllerBase
{
    private readonly AppDbContext _context;

    public DisciplinaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> Get() // async é necessário para operações assíncronas
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // IEnumerable<DisciplinaDTO> é o tipo de dados que será retornado, representando uma coleção de objetos DisciplinaDTO
    // DisciplinaDTO é um objeto de transferência de dados (DTO) que representa uma disciplina
    // e contém as propriedades que queremos expor na API
    {
        var disciplinas = await _context.Disciplinas
            .Include(d => d.Curso) // Aqui é onde incluímos o curso associado à disciplina, ele precias de ser carregado junto com a disciplina
            .Select(d => new  { // era para estar objeto DisciplinaDTO, mas como não tem o ID, não pode ser usado
                id = d.Id, // id é uma propriedade do objeto DisciplinaDTO que representa o ID da disciplina
                
                Descricao = d.Descricao, Curso = d.Curso.Descricao })
            .ToListAsync();

        return Ok(disciplinas);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] DisciplinaDTO dto)
    // [FromBody] indica que o objeto DisciplinaDTO será passado no corpo da requisição HTTP
    // DisciplinaDTO é um objeto de transferência de dados (DTO) que representa uma disciplina
    // e contém as propriedades que queremos expor na API
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    // Task<ActionResult> é o tipo de retorno para métodos assíncronos
    {
        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == dto.Curso);
        if (curso == null) return BadRequest("Curso não encontrado.");

        var disciplina = new Disciplina { Descricao = dto.Descricao, CursoId = curso.Id };
        _context.Disciplinas.Add(disciplina);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] DisciplinaDTO dto)
    // [FromBody] indica que o objeto DisciplinaDTO será passado no corpo da requisição HTTP
    // DisciplinaDTO é um objeto de transferência de dados (DTO) que representa uma disciplina
    // e contém as propriedades que queremos expor na API
    // ActionResult é uma classe base para retornar resultados de ações em controladores ASP.NET Core
    {
        var disciplina = await _context.Disciplinas.FindAsync(id);
        if (disciplina == null) return NotFound("Disciplina não encontrada.");

        var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == dto.Curso); // Verifica se o curso existe
        // FirstOrDefaultAsync retorna o primeiro elemento que satisfaz a condição especificada ou um valor padrão se nenhum elemento for encontrado
        
        if (curso == null) return BadRequest("Curso não encontrado.");

        disciplina.Descricao = dto.Descricao; // Atualiza a descrição da disciplina
        disciplina.CursoId = curso.Id;  // Atualiza o ID do curso associado à disciplina

        _context.Disciplinas.Update(disciplina); // Atualiza a disciplina no contexto do banco de dados
        await _context.SaveChangesAsync();  // Salva as alterações no banco de dados

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var disciplina = await _context.Disciplinas.FindAsync(id);
        if (disciplina == null) return NotFound("Disciplina não encontrada.");

        _context.Disciplinas.Remove(disciplina);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
