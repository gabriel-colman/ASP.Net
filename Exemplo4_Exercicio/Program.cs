using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Exemplo5ComBancoEntity.database;

namespace Exemplo5ComBancoEntity
{
    public class Executar
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Carrega string de conexão do appsettings.json
            // var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

            // Registra o DbContext com o PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));


            // Adiciona suporte a controllers e Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Erro para verificiar se teve conexão com o banco de dados
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    if (db.Database.CanConnect())
                    {
                        Console.WriteLine("✅ Conexão com o banco de dados estabelecida com sucesso.");
                        VerificarMapeamentoEntidades(db); // <-- chamada aqui
                    }
                    else
                    {
                        Console.WriteLine("❌ Não foi possível conectar com o banco de dados.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Falha ao conectar com o banco de dados.");
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }

            // Verifica se o banco de dados existe e cria se não existir
            void VerificarMapeamentoEntidades(AppDbContext db)
            {
                Console.WriteLine("🔍 Verificando mapeamento das entidades com o banco...");

                try
                {
                    // Tenta consultar cada entidade
                    _ = db.Usuarios.Take(1).ToList(); // isso aqui quer dizer que ele vai pegar 1 registro da tabela Usuarios,  Take é o mesmo que Limit no SQL
                    _ = db.Maquinas.Take(1).ToList();
                    _ = db.Softwares.Take(1).ToList();

                    Console.WriteLine("✅ Mapeamento entre entidades e tabelas verificado com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Erro no mapeamento das entidades:");
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }



            // Habilita Swagger (ambiente dev)
            // if (app.Environment.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            // }

            // Habilita HTTPS
            app.UseHttpsRedirection();

            // Habilita arquivos estáticos da pasta wwwroot (html, css, js)
            app.UseDefaultFiles(); // Procura por index.html
            app.UseStaticFiles();  // Permite servir arquivos de wwwroot

            // Habilita autenticação/autorização (mesmo que ainda não usada)
            app.UseAuthorization();

            // Mapeia os endpoints da API
            app.MapControllers();

            // Roda a aplicação
            app.Run();

            // link para o swagger: http://localhost:5000/swagger/index.html
        }
    }
}
