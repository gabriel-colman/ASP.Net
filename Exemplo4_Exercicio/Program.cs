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
            using (var scope = app.Services.CreateScope()) // CreateScope cria um escopo para o DbContext(escopo seria um contexto de execução temporário, onde o DbContext é criado e usado)
            // permitindo que ele seja descartado corretamente após o uso
            // Isso é importante para evitar problemas de conexão com o banco de dados, especialmente em aplicações ASP.NET Core onde o DbContext é registrado como um serviço com escopo (Scoped).
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Obtém uma instância do DbContext a partir do escopo criado
                try
                {
                    if (db.Database.CanConnect()) // Verifica se o banco de dados está acessível
                    // Se o banco de dados estiver acessível, a conexão foi estabelecida com sucesso
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
                    _ = db.Usuarios.Take(1).ToList(); // isso aqui quer dizer que ele vai pegar 1 registro da tabela Usuarios,  Take é o mesmo que Limit no SQL, _= quer dizer que não vai usar o retorno
                    // ou seja, ele só quer verificar se a tabela existe e se o mapeamento está correto
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
