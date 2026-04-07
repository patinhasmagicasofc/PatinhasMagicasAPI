using System.Security.Claims;

namespace PatinhasMagicasPWA.Services
{
    public class JwtService
    {
        private readonly TokenStorageService _tokenStorageService;
        private readonly JwtTokenParserService _jwtTokenParserService;

        public JwtService(TokenStorageService tokenStorageService, JwtTokenParserService jwtTokenParserService)
        {
            _tokenStorageService = tokenStorageService;
            _jwtTokenParserService = jwtTokenParserService;
        }

        public async Task<string?> GetUserIdAsync()
        {
            var token = await _tokenStorageService.GetToken();

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
            return _jwtTokenParserService.ParseClaims(jwt);
        }

    }
}
