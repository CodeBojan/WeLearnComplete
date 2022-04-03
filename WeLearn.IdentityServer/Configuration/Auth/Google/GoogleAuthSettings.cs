namespace WeLearn.IdentityServer.Configuration.Auth.Google
{
    public record GoogleAuthSettings
    {
        public const string SectionName = nameof(GoogleAuthSettings);
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
