namespace SelfieAWookieApi.Applications.DTO
{
    public class AuthDTO
    {
        public required string Login { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Tokken { get; set; } 
    }
}
