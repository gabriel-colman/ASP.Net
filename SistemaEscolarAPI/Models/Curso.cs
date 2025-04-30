using System.Collections.Generic; // Para instalar os pacotes necessários, use os seguintes comandos:
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
namespace SistemaEscolarAPI.Models;



public class Curso
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public ICollection<Aluno> Alunos { get; set; }
    public ICollection<Disciplina> Disciplinas { get; set; }
}
