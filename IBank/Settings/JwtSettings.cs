namespace IBank.Settings
{
    public class JwtSettings : IJwtSettings
    {
        public string Secret { get; set; }
        public string ExpirationInSeconds { get; set; }
    }
}
