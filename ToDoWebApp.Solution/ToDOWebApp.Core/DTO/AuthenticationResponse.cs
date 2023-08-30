namespace ToDOWebApp.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
