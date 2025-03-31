# ASP.Net

Este repositório contém exemplos e exercícios desenvolvidos durante as aulas na Digix sobre ASP.NET. Cada pasta representa um projeto com diferentes funcionalidades e abordagens para o desenvolvimento de aplicações web utilizando o framework ASP.NET Core.

## Projetos

### 1. Exemplo
Este projeto é um exemplo básico de configuração de uma aplicação ASP.NET Core. Ele inclui:
- Configuração de middlewares para servir arquivos estáticos.
- Redirecionamento para uma página inicial (`index.html`) localizada na pasta `wwwroot`.
- Uso de um design simples com HTML, CSS e JavaScript.

**Arquivos principais:**
- `Program.cs`: Configuração básica da aplicação.
- `wwwroot/index.html`: Página inicial do projeto.
- `wwwroot/css/style.css`: Estilo da página inicial.
- `wwwroot/css/script.js`: Script para animações no cursor.

---

### 2. Exemplo_2_ASPNET_ENDPOINT
Este projeto demonstra como criar uma API REST simples com ASP.NET Core. Ele inclui:
- Um controlador para gerenciar usuários (`UsuarioController`).
- Endpoints para operações CRUD (Create, Read, Update, Delete).
- Uso do Swagger para documentação da API.

**Arquivos principais:**
- `Program.cs`: Configuração da aplicação e habilitação do Swagger.
- `Controller/UsuarioController.cs`: Implementação dos endpoints CRUD.
- `Models/Usuario.cs`: Modelo de dados para usuários.

---

### 3. Exemplo_3_Endpoint_ASPNET_Banco
Este projeto expande o exemplo anterior, integrando um banco de dados PostgreSQL com o Entity Framework Core. Ele inclui:
- Configuração de uma conexão com o banco de dados.
- Mapeamento de entidades para tabelas do banco.
- Endpoints CRUD para gerenciar usuários armazenados no banco.

**Arquivos principais:**
- `Program.cs`: Configuração da aplicação e integração com o banco de dados.
- `database/AppDbContext.cs`: Configuração do contexto do banco de dados.
- `Controller/UsuarioController.cs`: Endpoints CRUD para usuários.
- `Models/Usuario.cs`: Modelo de dados mapeado para a tabela `usuario`.

---

### 4. Exemplo4_Exercicio
Este projeto é um exercício prático que implementa um sistema completo de gerenciamento de usuários, máquinas e softwares. Ele inclui:
- Integração com um banco de dados PostgreSQL.
- Mapeamento de entidades para tabelas do banco.
- Endpoints CRUD para gerenciar usuários, máquinas e softwares.
- Uma interface web para interagir com os dados.

**Funcionalidades:**
- Gerenciamento de usuários, máquinas e softwares com relacionamentos entre as tabelas.
- Interface web para cadastro, edição e exclusão de usuários.
- Uso do Swagger para documentação da API.

**Arquivos principais:**
- `Program.cs`: Configuração da aplicação e integração com o banco de dados.
- `database/AppDbContext.cs`: Configuração do contexto do banco de dados.
- `controllers/UsuarioController.cs`: Endpoints CRUD para usuários.
- `controllers/MaquinaController.cs`: Endpoints CRUD para máquinas.
- `controllers/SoftwareController.cs`: Endpoints CRUD para softwares.
- `wwwroot/index.html`: Interface web para gerenciar usuários.
- `wwwroot/script.js`: Lógica para interagir com a API via interface web.
- `scriptBanco.sql`: Script para criação das tabelas e inserção de dados no banco.

---

## Como executar os projetos

1. Certifique-se de ter o .NET SDK instalado.
2. Navegue até a pasta do projeto desejado.
3. Execute o comando `dotnet run` para iniciar a aplicação.
4. Para projetos com banco de dados, configure a string de conexão no arquivo `appsettings.json`.

---

## Tecnologias utilizadas
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger
- HTML, CSS e JavaScript