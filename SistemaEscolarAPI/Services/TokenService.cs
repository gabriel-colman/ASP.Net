using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEscolarAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace SistemaEscolarAPI.Services
{
    public class TokenService
    {
        public static string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // Criando um manipulador de tojer jwt

            var key = Encoding.ASCII.GetBytes("minha-chave-ultra-segura-com-32-caracteres");
            // Chave secreta para assinar o token. Deve ser mantida em segredo e não dese ser exposta
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // SecurityTokenDescriptor é uma classe que contem informações sobre o token, aqui pode colocar o tempo que expira o token, as credencias de assinatura
                Subject = new ClaimsIdentity(new Claim[] {
                    // ClaimsIdentity é uma classe que representa uma identidade de usuario com um cojunto  de reividicações
                    new Claim(ClaimTypes.Name, usuario.Username)
                    // Uma reividicação (claim) é uma declaração sobre um usuairo, com seu nome ou funções
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Aqui ta definindo o tempo de expiração do token que vai ficar 2 horas do momento que foi chamado.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    // SymmetricSecurityKey é uma classe que represata uma chave simetrica usada para a assinatura do token
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            //  WriteToken é metodo quer serializar o token JWT em string
            // Serializar é o processo de converter um objeto em uma representação string ou binaria de armazenamento ou transmissão
            // O token deve ser enviano no cabeçalho da Autorização das requisições HTTP.
        }
    }
}