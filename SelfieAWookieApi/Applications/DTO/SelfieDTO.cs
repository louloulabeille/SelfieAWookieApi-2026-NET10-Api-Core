namespace SelfieAWookieApi.Applications.DTO
{
    // Classe DTO (Data Transfer Object) pour représenter les données d'un selfie
    // Cette classe peut être utilisée pour transférer les données entre les différentes couches de l'application
    // et pour éviter d'exposer directement les entités de domaine dans les contrôleurs ou les services
    [Serializable]
    public class SelfieDTO
    {
        #region properties
        public required string? Title { get; set; }
        public int? WookieId { get; set; }

        public int? NbSelfieFromWookie { get; set; }

        #endregion
    }
}
