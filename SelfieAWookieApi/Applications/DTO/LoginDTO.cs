namespace SelfieAWookieApi.Applications.DTO
{
    public class LoginDTO
    {
        public required string Login {  get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }

        public string? Token { get; set; }
    }
}
