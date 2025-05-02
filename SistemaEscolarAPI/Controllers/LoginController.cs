using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarAPI.DTOs;
using SistemaEscolarAPI.Models;
using SistemaEscolarAPI.Services;
using FluentValidation;

namespace SistemaEscolarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {   
        [HttpPost]
        public IActionResult Login ([FromBody] LoginDTO loginDto) {
            // IActionResult é uma interface que representa o resultado de uma ação em controlador no AspNet

            // IsNullOrWhiteSpace verifica se  a string é nula ou contem apenas espaços em branco
            if (string.IsNullOrWhiteSpace(loginDto.Username)|| string.IsNullOrWhiteSpace(loginDto.Password)) {
                return BadRequest("Usuarios e senha são obrigatorios"); // aqui também retorna 400 com a mensagem incluida
            }

            var users = new List<Usuario>
            {
                new Usuario {Username = "admin", Password = "123", Role = "Adminitrador"},
                new Usuario {Username = "func", Password = "123", Role = "Funcionario"}
            }

            var user = users.FirstOrDefault( u => 
                u.Username == loginDto.Username &&
                u.Password == loginDto.Password 
            );

            if (user == null)
                return Unauthorized(new {message = "Usuario ou senha invalida"})
            // Unauthorized retorna 401 com a mensagem informado que a validação não é a correta

            var token = TokenService.GenerateToken(user);
            return Ok(new {token});


        }
    }
}