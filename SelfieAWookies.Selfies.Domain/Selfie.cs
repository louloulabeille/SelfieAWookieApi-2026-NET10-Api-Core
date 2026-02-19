namespace SelfieAWookies.Selfies.Domain
{
    [Serializable]
    public class Selfie
    {
        #region Properties

        public int Id { get; set; }
        public required string Title { get; set; } 
        public string? ImagePath { get; set; }

        public int WookieId { get; set; }
        public required Wookie Wookie { get; set; }

        #endregion
    }
}
