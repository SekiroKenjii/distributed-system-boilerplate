namespace OAuthServer;

public class AuthCode
{
    public string ClientId { get; set; } = string.Empty;
    public string CodeChallenge { get; set; } = string.Empty;
    public string CodeChallengeMethod { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}