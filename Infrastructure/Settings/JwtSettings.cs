namespace Infrastructure.Settings
{
    public class JwtSettings
    {
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
