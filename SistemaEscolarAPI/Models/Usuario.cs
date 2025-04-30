namespace SistemaEscolarAPI.Models;


// Aqui vamos modelo simples para login com role que é o que o Identity faz
// mas não vamos usar o Identity para não complicar o projeto
// o projeto é só para estudo e não precisa de segurança, então não vamos usar o Identity
// o Identity é muito complexo e não precisamos dele para esse projeto

public class Usuario
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
// role é cargo do usuário, pode ser aluno, professor ou admin