using System.Security.Claims;
using System.Text.Json;

namespace PatinhasMagicasPWA.Services
{
    public class JwtService
    {
        private readonly TokenStorageService _tokenStorageService;

        public JwtService(TokenStorageService tokenStorageService)
        {
            _tokenStorageService = tokenStorageService;
        }

        public async Task<string?> GetUserIdAsync()
        {
            var token = await _tokenStorageService.GetToken();
            token = SanitizeToken(token);

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var claims = ParseClaims(token);

            return claims.FirstOrDefault(claim =>
                claim.Type == ClaimTypes.NameIdentifier ||
                claim.Type == "nameid" ||
                claim.Type == "sub")?.Value;
        }

        public IEnumerable<Claim> ParseClaims(string jwt)
        {
            var parts = jwt.Split('.');

            if (parts.Length < 2)
            {
                return Enumerable.Empty<Claim>();
            }

            var jsonBytes = ParseBase64WithoutPadding(parts[1]);
            var claimsDictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);

            if (claimsDictionary is null)
            {
                return Enumerable.Empty<Claim>();
            }

            return claimsDictionary.Select(claim => new Claim(claim.Key, claim.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }

        private static string? SanitizeToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            return token.Trim().Trim('"');
        }
    }
}
