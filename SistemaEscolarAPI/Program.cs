// Comando para colcoar os migrations no banco de dados
// dotnet ef migrations add InitialCreate
// dotnet ef database update
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using System.Text;
using SistemaEscolarAPI.DB;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext com PostgreSQL (usando appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Adicionar serviços ao contêiner
builder.Services.AddControllers(); // Adiciona suporte a controladores
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => // para gerar a documentação lá em baixar, que é o Schemas
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sistema Escolar API", Version = "v1" }); // Define o título e a versão da API na documentação do Swagger
});


// JWT Autenticação(na qual já temos o TokenService)
// AddJwtBearer é metodo que configura a autenticação jwt para o aplicativo ASPNET Core. Ele permite que o aplicativo valide o jwt recebido nas solicitação http.
// JwtBearerDefaults é um sistema de autenticação padrão dos tokens
// AuthenticationScheme é um parametro que especifica o esquema de autenticação usado. Neste caso nosso estamous usando o do jwt.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters //Configura os parametros de validação do token Jwt
        {
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do emissor
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("minha-chave-ultra-segura-com-32-caracteres")), // Chave secreta para assinar o token. Deve ser mantida em segredo e não deve ser exposta no código-fonte.
            ValidateIssuer = false, // Valida o emissor do token (não é necessário para este exemplo)
            ValidateAudience = false    // Valida o público do token (não é necessário para este exemplo)
        };
    });

var app = builder.Build();

// Configurar middlewares
app.UseSwagger();
app.UseSwaggerUI();


// Redirecionemanto para o front end
app.UseAuthentication(); // Ativa a autenticação, que valida os tokens Jwt nas solicitações recebidas. Isso garante apenas os usuarios autenticados ter acesso a api
app.UseAuthorization();

// Para subir os arquivos estaticos
app.UseStaticFiles(); // Permite que os arquivps estaticos diretamente para o cliente
app.UseRouting(); // roteamente qeue permite que o ASP direcioen as solictação para os controladores apropriados com base nas rotas definidas
app.UseHttpsRedirection(); // Redirecionamento https. que redireciona automaticamete as solicitações htt´ps

app.MapGet("/", context =>
{
    context.Response.Redirect("/login.html");
    return Task.CompletedTask;
}
);


app.MapControllers(); // Mapeia os controladores
app.Run();

// Link para Swagger: http://localhost:5164/swagger/index.html