using Microsoft.IdentityModel.Tokens;
using PatinhasMagicasAPI.Models;
using PatinhasMagicasAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatinhasMagicasAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecret;

        public TokenService(IConfiguration configuration)
        {
            _jwtSecret = configuration["Jwt:Key"] ??
                         throw new InvalidOperationException("A chave secreta JWT não foi configurada.");
        }

        public string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
               new Claim(ClaimTypes.Name, usuario.Nome),                            
               new Claim(ClaimTypes.Email, usuario.Email),                         
               new Claim(ClaimTypes.Role, usuario.TipoUsuario?.DescricaoTipoUsuario ?? "Cliente")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}