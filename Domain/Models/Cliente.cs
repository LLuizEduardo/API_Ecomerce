namespace API.Domain.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string? Endereco { get; set; }
        public string? Email { get; set; }
    }
}
