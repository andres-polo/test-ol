namespace ComercioApi.Application.Configuration;

public class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = "ComercioApi";
    public string Audience { get; set; } = "ComercioApi";
    public int ExpirationMinutes { get; set; } = 60;
}
