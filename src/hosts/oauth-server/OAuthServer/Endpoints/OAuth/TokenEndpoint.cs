using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace OAuthServer.Endpoints.OAuth;

public static class TokenEndpoint
{
    public static async Task<IResult> Handle(
        HttpRequest request,
        DevKeys devKeys,
        IDataProtectionProvider dataProtectionProvider)
    {
        var bodyBytes = await request.BodyReader.ReadAsync();
        var bodyContent = Encoding.UTF8.GetString(bodyBytes.Buffer);
        var grantType = string.Empty;
        var code = string.Empty;
        var redirectUri = string.Empty;
        var codeVerifier = string.Empty;

        foreach (var part in bodyContent.Split('&')) {
            var subParts = part.Split('=');
            var key = subParts[0];
            var value = subParts[1];

            switch (key) {
                case "grant_type":
                    grantType = value;
                    break;
                case "code":
                    code = value;
                    break;
                case "redirect_uri":
                    redirectUri = value;
                    break;
                case "code_verifier":
                    codeVerifier = value;
                    break;
            }
        }

        var protector = dataProtectionProvider.CreateProtector("oauth");
        var codeString = protector.Unprotect(code);
        var authCode = JsonSerializer.Deserialize<AuthCode>(codeString);

        if (authCode == null || !ValidateCodeVerifier(authCode, codeVerifier)) return Results.BadRequest();

        var handler = new JsonWebTokenHandler();

        return Results.Ok(new {
            access_token = handler.CreateToken(new SecurityTokenDescriptor {
                Claims = new Dictionary<string, object> {
                    [JwtRegisteredClaimNames.Sub] = Guid.NewGuid().ToString(), // FIXME: Use a real user ID
                    ["custom"] = "data"                                        // Add custom claims here
                },
                Issuer = "https://localhost:7256",
                Expires = DateTime.UtcNow.AddMinutes(15),
                TokenType = "Bearer",
                SigningCredentials = new SigningCredentials(devKeys.RsaSecurityKey, SecurityAlgorithms.RsaSha256)
            }),
            token_type = "Bearer"
        });
    }

    private static bool ValidateCodeVerifier(AuthCode code, string codeVerifier)
    {
        var codeChallenge = Base64UrlEncoder.Encode(SHA256.HashData(Encoding.ASCII.GetBytes(codeVerifier)));

        return code.CodeChallenge == codeChallenge;
    }
}