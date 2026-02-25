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
        public Wookie? Wookie { get; set; }

        public Guid? PictureId { get; set; }
        public Picture? Picture { get; set; }

        #endregion
    }
}
