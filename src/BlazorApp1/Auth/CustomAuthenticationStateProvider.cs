using BlazorApp1.Auth;
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ProtectedLocalStorage _localStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, ProtectedLocalStorage localStorage)
    {
        _sessionStorage = sessionStorage;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionLocalStorageResult = await _localStorage.GetAsync<UserSession>("User Session");
            if (userSessionLocalStorageResult.Success && userSessionLocalStorageResult.Value != null)
            {
                var userSession = userSessionLocalStorageResult.Value;
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, userSession.Id),
                    new Claim(ClaimTypes.Name, userSession.FirstName),
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, "CustomAuth"));

                return new AuthenticationState(claimsPrincipal);
            }

            var userSessionSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("User Session");
            var userSessionSession = userSessionSessionStorageResult.Success ? userSessionSessionStorageResult.Value : null;
            if (userSessionSession == null)
            {
                return new AuthenticationState(_anonymous);
            }

            var claimsPrincipalSession = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userSessionSession.Id),
                new Claim(ClaimTypes.Name, userSessionSession.FirstName),
                new Claim(ClaimTypes.Email, userSessionSession.Email),
                new Claim(ClaimTypes.Role, userSessionSession.Role)
            }, "CustomAuth"));

            return new AuthenticationState(claimsPrincipalSession);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task<ThemeManagerModel?> GetThemeSettingsAsync()
    {
        var themeSettingsResult = await _localStorage.GetAsync<ThemeManagerModel>("ThemeSettings");
        return themeSettingsResult.Success ? themeSettingsResult.Value : new ThemeManagerModel();
    }

    public async Task UpdateThemeSettingsAsync(ThemeManagerModel themeSettings)
    {
        await _localStorage.SetAsync("ThemeSettings", themeSettings);
    }


    public async Task UpdateAuthenticationStateAsync(UserSession userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            await _localStorage.SetAsync("User Session", userSession);
            await _sessionStorage.SetAsync("User Session", userSession);

            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Sid, userSession.Id),
                new Claim(ClaimTypes.Name, userSession.FirstName),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role)
            }));
        }
        else
        {
            await _localStorage.DeleteAsync("User Session");
            await _sessionStorage.DeleteAsync("User Session");
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}