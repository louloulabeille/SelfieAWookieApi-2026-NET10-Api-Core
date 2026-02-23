using SelfieAWookies.Selfies.Domain;

namespace SelfieAWookieApi.Applications.DTO
{
    public class SelfieDTOComplete
    {
        #region properties
        public int Id { get; set; }
        public required string? Title { get; set; }
        public string? ImagePath { get; set; }
        public int WookieId { get; set; }
        public Wookie? Wookie { get; set; }
        #endregion
    }
}
