namespace API.Domain.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataEnvio { get; set; }
        public Cliente Cliente { get; set; } = new();
        public EStatus Status { get; set; }
        public InfoEnvio InformacaoEnvio { get; set; } = new();
        //public string? Estado { get; set; }
    }

    public enum EStatus
    {
        Realizado = 1,
        Processamento = 2,
        Finalizado = 3,
    }
}
