namespace SelfieAWookieModels
{
    // This class is a simple model representing a selfie, with an Id property.
    // It is marked as serializable, which means it can be easily converted to
    // and from formats like JSON or XML for storage or transmission.
    [Serializable]
    public class Selfie
    {
        public int Id { get; set; }
    }
}
