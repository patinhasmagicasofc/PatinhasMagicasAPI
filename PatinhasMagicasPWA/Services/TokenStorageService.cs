using Microsoft.JSInterop;

namespace PatinhasMagicasPWA.Services
{
    public class TokenStorageService
    {
        private const string TokenKey = "authToken";
        private readonly IJSRuntime _jsRuntime;
        private readonly JwtTokenParserService _jwtTokenParserService;
        private readonly AuthNavigationService _authNavigationService;

        public TokenStorageService(
            IJSRuntime jsRuntime,
            JwtTokenParserService jwtTokenParserService,
            AuthNavigationService authNavigationService)
        {
            _jsRuntime = jsRuntime;
            _jwtTokenParserService = jwtTokenParserService;
            _authNavigationService = authNavigationService;
        }

        public async Task SetToken(string token)
        {
            var normalizedToken = NormalizeToken(token);

            if (string.IsNullOrEmpty(normalizedToken))
            {
                await RemoveToken();
                return;
            }

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, normalizedToken);
        }

        public async Task<string?> GetToken()
        {
            var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
            var normalized = NormalizeToken(token);

            if (string.IsNullOrEmpty(normalized))
            {
                return null;
            }

            if (_jwtTokenParserService.IsExpired(normalized))
            {
                await RemoveToken();
                _authNavigationService.RedirectToLoginIfNeeded();
                return null;
            }

            return normalized;
        }

        public async Task RemoveToken()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }

        private static string NormalizeToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return string.Empty;
            }

            return token.Trim().Trim('"');
        }

    }
}
