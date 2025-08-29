namespace Application.Common.Models.User
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public string? ErrorMessage { get; set; }

        public bool IsSuccessful { get => string.IsNullOrWhiteSpace(ErrorMessage); }
    }
}
