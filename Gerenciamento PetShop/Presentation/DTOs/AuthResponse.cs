namespace Gerenciamento_PetShop.Presentation.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; } 
    }
}
