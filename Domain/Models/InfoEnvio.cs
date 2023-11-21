namespace API.Domain.Models
{
    public class InfoEnvio
    {
        public int Id { get; set; }
        public double CustoEnvio { get; set; }
        public ETipoEnvio TipoEnvio { get; set; }
        public string CodEnvio { get; set; } = string.Empty;
    }

    public enum ETipoEnvio
    {
        Correio = 1,
        Transportadora = 2,
        Retirada = 3
    }
}
