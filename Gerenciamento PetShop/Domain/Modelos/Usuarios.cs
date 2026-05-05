namespace Gerenciamento_PetShop.Domain.Modelos
{
    public class Usuarios
    {
        public Usuarios(string cpf, string senha)
        {
            Cpf = cpf;
            Senha = senha;
        }

        public int Id{ get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }
}
