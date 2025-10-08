using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Interfaces; // Necessário se estiver usando IConfiguration

namespace PatinhasMagicasAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecret;

        // Construtor (assumindo que você está injetando a chave secreta)
        // No seu TokenService.cs
        public TokenService(IConfiguration configuration)
        {
            // Deve ler de "Jwt:Key"
            _jwtSecret = configuration["Jwt:Key"] ??
                         throw new InvalidOperationException("A chave secreta JWT não foi configurada.");
        }

        public string GenerateToken(Usuario usuario)
        {
            // 1. Configurar os claims (informações do usuário)
            var claims = new List<Claim>
            {
                // Claim de identificação única do usuário no token
                new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString()), 
                
                // Claim do Nome
                new Claim(ClaimTypes.Name, usuario.Nome), 
                
                // Claim do Email (muitas vezes usado para identificação principal)
                new Claim(ClaimTypes.Email, usuario.Email),
                
                // Claim de PERFIL (ROLE)
                // Usamos 'usuario.Role' se você adicionou uma propriedade calculada (Opção 2 anterior)
                // OU usamos a propriedade do modelo de navegação:
                new Claim(ClaimTypes.Role, usuario.TipoUsuario?.DescricaoTipoUsuario ?? "Cliente") 
                
                // O padrão é 'Cliente' caso a descrição do tipo de usuário seja nula.
            };

            // 2. Criar a chave de segurança
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Criar a descrição do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), // Validade de 2 horas
                SigningCredentials = creds
            };

            // 4. Gerar e retornar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}