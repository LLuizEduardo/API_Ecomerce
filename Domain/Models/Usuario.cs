namespace API.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime? DataCadastro { get; set; }
    }
}