using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Unohana.Services
{
    public class JwtAuthStateService(ISessionStorageService storageService) : AuthenticationStateProvider
    {
        private ISessionStorageService _storageService => storageService;
        private ClaimsPrincipal _anonymous => new ClaimsPrincipal(
            new ClaimsIdentity()
            );

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _storageService.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(_anonymous);
            }
            // success
            var identity = new ClaimsIdentity(JwtParser.ParseClaims)
        }
    }
}
