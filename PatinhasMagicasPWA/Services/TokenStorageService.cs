using Microsoft.JSInterop;

namespace PatinhasMagicasPWA.Services
{
    public class TokenStorageService
    {
        private const string TokenKey = "authToken";
        private readonly IJSRuntime _jsRuntime;

        public TokenStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetToken(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }

        public async Task<string?> GetToken()
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }

        public async Task RemoveToken()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
    }
}
