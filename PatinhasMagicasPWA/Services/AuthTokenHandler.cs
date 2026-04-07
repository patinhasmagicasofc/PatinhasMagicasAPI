using System.Net.Http.Headers;
using System.Net;

namespace PatinhasMagicasPWA.Services
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly TokenStorageService _tokenStorageService;
        private readonly AuthNavigationService _authNavigationService;

        public AuthTokenHandler(TokenStorageService tokenStorageService, AuthNavigationService authNavigationService)
        {
            _tokenStorageService = tokenStorageService;
            _authNavigationService = authNavigationService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenStorageService.GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _tokenStorageService.RemoveToken();
                _authNavigationService.RedirectToLoginIfNeeded();
            }

            return response;
        }
    }
}
