using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.DTOs
{
    public class DisciplinaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Curso { get; set; }
    }
}

/*

O DTO (Data Transfer Object) é um padrão de design usado para transferir dados entre diferentes camadas de uma aplicação, como entre o backend e o frontend. Ele é uma representação simplificada de um objeto de domínio, contendo apenas os dados necessários para uma operação específica. No caso de APIs, o DTO é frequentemente usado para estruturar os dados enviados e recebidos em requisições HTTP.

Por que é importante ter o ID no DTO?
Identificação de Recursos:

O Id é essencial para identificar de forma única um recurso (neste caso, um aluno) no sistema. Ele permite que operações como atualização (PUT) e exclusão (DELETE) sejam realizadas de forma precisa.
Operações no Frontend:

No frontend, o Id é usado para associar ações específicas a um recurso. Por exemplo, ao clicar no botão "Editar" ou "Excluir", o frontend precisa saber qual aluno está sendo manipulado, e o Id é a chave para isso.
Consistência e Integração:

O Id garante que o backend e o frontend estejam sincronizados em relação aos dados. Sem ele, seria difícil garantir que as operações realizadas no frontend correspondam ao recurso correto no backend.
Evita Ambiguidade:

Em sistemas com múltiplos registros, como uma lista de alunos, o Id elimina a ambiguidade ao identificar um registro específico, mesmo que outros campos (como Nome) possam ser iguais.
Exemplo prático:
No seu caso, o AlunoDTO com o Id permite que:

O frontend saiba qual aluno está sendo editado ou excluído.
O backend receba o Id para localizar o registro correto no banco de dados.
Sem o Id, seria necessário confiar em outros campos (como Nome), o que pode levar a erros caso existam registros duplicados ou inconsistências nos dados.


*/