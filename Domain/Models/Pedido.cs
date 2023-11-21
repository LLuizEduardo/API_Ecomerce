namespace API.Domain.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataEnvio { get; set; }
        public Cliente Cliente { get; set; } = new();
        public string? Estado { get; set; }
        //public int Quantidade { get; set; }
    }
}
