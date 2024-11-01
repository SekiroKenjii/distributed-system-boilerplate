using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.DataProtection;

namespace OAuthServer.Endpoints.OAuth;

public static class AuthorizationEndpoint
{
    public static IResult Handle(HttpRequest request, IDataProtectionProvider dataProtectionProvider)
    {
        var iss = HttpUtility.UrlEncode("https://localhost:7256");
        request.Query.TryGetValue("state", out var state);


        if (!request.Query.TryGetValue("response_type", out _))
            // TODO: check if response_type is valid

            return Results.BadRequest(new {
                error = "invalid_request",
                error_description = "response_type is required",
                state,
                iss
            });

        request.Query.TryGetValue("client_id", out var clientId); // TODO: check if client_id is valid
        request.Query.TryGetValue("code_challenge", out var codeChallenge);
        request.Query.TryGetValue("code_challenge_method", out var codeChallengeMethod);
        request.Query.TryGetValue("redirect_uri", out var redirectUri);
        request.Query.TryGetValue("scope", out var scope); // TODO: check if scope is valid

        var protector = dataProtectionProvider.CreateProtector("oauth");
        var code = new AuthCode {
            ClientId = clientId.ToString(),
            CodeChallenge = codeChallenge.ToString(),
            CodeChallengeMethod = codeChallengeMethod.ToString(),
            RedirectUrl = redirectUri.ToString(),
            Expiry = DateTime.UtcNow.AddMinutes(5)
        };
        var codeString = protector.Protect(JsonSerializer.Serialize(code));

        return Results.Redirect($"{redirectUri}?code={codeString}&state={state}&iss={iss}");
    }
}