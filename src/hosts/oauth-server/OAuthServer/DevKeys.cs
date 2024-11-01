using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace OAuthServer;

public class DevKeys
{
    public DevKeys(IWebHostEnvironment environment)
    {
        RsaKey = RSA.Create();
        var path = Path.Combine(environment.ContentRootPath, "crypto_key");

        if (File.Exists(path)) {
            var rsaKey = RSA.Create();
            rsaKey.ImportRSAPrivateKey(File.ReadAllBytes(path), out _);

            return;
        }

        File.WriteAllBytes(path, RsaKey.ExportRSAPrivateKey());
    }

    private RSA? RsaKey { get; }

    public RsaSecurityKey RsaSecurityKey => new(RsaKey);
}