using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Exemplo_2_ASPNET_ENDPOINT.Controller;

// Para executar os comandos eu preciso instalar os pacotes ASP.NET Core com o comando: dotnet add package Microsoft.AspNetCore

// Vamos usar a ferramenta Swagger para documentar a API, que já esta incluida no ASP.NET Core, mas precisa de um pacote para funcionar, e nisso precisamos executa o comando: dotnet add package Swashbuckle.AspNetCore

// Chamar as bibliotecas do ASP.NET Core
using Microsoft.AspNetCore.Builder; // Isso server para configurar a aplicação e interfaces e classes para configurar a aplicação
using Microsoft.Extensions.Hosting; // Isso server para configurar a aplicação e interfaces e classes para configurar a aplicação
using Microsoft.Extensions.DependencyInjection; // Isso server para configurar a aplicação e interfaces 


namespace Exemplo_2_ASPNET_ENDPOINT
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            // Vou chamar uma classe selead com o nome WebApplication, que é uma classe que representa uma aplicação web ASP.NET Core
            var builder = WebApplication.CreateBuilder(args); // Vai criar uma aplicação web
            // o args porque ele é um array de string que representa os argumentos da linha de comando

            // Agora vou adicionar o serviços de contoller do WebApplication
            builder.Services.AddControllers(); // Vai adicionar os serviços de contoller do WebApplication

            // Habilitar o Swagger para documetação da API
            builder.Services.AddEndpointsApiExplorer(); // Vai adicionar o Swagger para documetação da API
            builder.Services.AddSwaggerGen(); // Vai adicionar o Swagger para documetação da API

            var app = builder.Build(); // Vai construir a aplicação

            app.UseSwagger(); // Vai usar o Swagger
            app.UseSwaggerUI(); // Vai usar o Swagger

            app.UseHttpsRedirection(); // Vai usar o HTTPS, adiciona o middleware de redirecionamento HTTPS (que é um protocolo de comunicação de segurança sobre uma rede de computadores), exemplo: http://localhost:5000 para https://localhost:5001

            app.UseAuthorization(); // Vai usar a autorização, adiciona o middleware de autorização que permite a autenticação do usuário

            app.MapControllers(); // Vai mapear os contollers, adiciona o middleware de roteamento que corresponde a solicitações HTTP a um controlador

            app.Run(); // Vai rodar a aplicação

            // O já pré estabelecido pela API do ASP.NET Core para entrar no Swagger é: https://localhost:5001/swagger/index.html

            // Executar no swagger: https://localhost:5001/swagger/index.html
        }
    }
}