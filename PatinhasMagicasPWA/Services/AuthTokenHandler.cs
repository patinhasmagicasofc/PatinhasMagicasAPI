using System.Net.Http.Headers;

namespace PatinhasMagicasPWA.Services
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly TokenStorageService _tokenStorageService;

        public AuthTokenHandler(TokenStorageService tokenStorageService)
        {
            _tokenStorageService = tokenStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStorageService.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
