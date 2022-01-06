namespace IBank.Settings
{
    public interface IJwtSettings
    {
        public string Secret { get; set; }
        public string ExpirationInSeconds { get; set; }
    }
}
