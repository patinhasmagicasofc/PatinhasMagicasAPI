using Microsoft.AspNetCore.Components;

namespace PatinhasMagicasPWA.Services
{
    public class AuthNavigationService
    {
        private readonly NavigationManager _navigation;

        public AuthNavigationService(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public void RedirectToLoginIfNeeded()
        {
            var currentPath = new Uri(_navigation.Uri).AbsolutePath;

            if (!string.Equals(currentPath, "/Login", StringComparison.OrdinalIgnoreCase))
            {
                _navigation.NavigateTo("/Login", replace: true);
            }
        }
    }
}
