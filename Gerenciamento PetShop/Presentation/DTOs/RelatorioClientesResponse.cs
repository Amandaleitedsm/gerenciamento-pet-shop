namespace Gerenciamento_PetShop.Presentation.DTOs
{
    public class RelatorioClientesResponse
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int QuantidadePets { get; set; }
    }
}
