namespace API.Domain.Models
{
    public class InformacaoEnvio
    {
        public int Id { get; set; }
        public double CustoEnvio { get; set; } 
        public int TipoEnvio { get; set; }
        public int NumeroEnvio { get; set; }
    }
}
