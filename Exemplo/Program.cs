using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// Para adicionar a dependencias do AspNetCore
// dotnet add package Microsoft.AspNetCore.App

// Bibliotecas para usar o AspNetCore
using Microsoft.AspNetCore.Builder; // Para configurar a aplicação
using Microsoft.AspNetCore.Hosting; // Para hospedar a aplicação
using Microsoft.Extensions.Hosting; // A Extensão do Host é necessária para hospedar a aplicação, usando o IHostBuilder
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http; // Para adicionar serviços à aplicação

namespace Exemplo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); // Criar o app
            var app = builder.Build();

            // Configurar middlewares
            app.UseStaticFiles(); // Habilita arquivos estáticos de wwwroot
            app.UseRouting(); // Habilita roteamento

            // app.MapGet("/", () => "Hello World!"); // Responde com "Hello World!"

            // Definir rotas
            app.UseEndpoints(endpoints => // Define as rotas
            {
                endpoints.MapGet("/", async context => // Rota que responde a requisções GET
                {
                    // app.Map("/", () => "Hello World!"); // Responde com "Hello World!"
                    context.Response.Redirect("/index.html"); // Redireciona para a página inicial
                });
            });

            app.Run(); // Executa a aplicação
        }
    }
}