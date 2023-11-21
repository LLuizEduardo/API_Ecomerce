namespace API.Domain.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public Produto Produto { get; set; } = new Produto();
        public int Quantidade { get; set; }
        public DateTime DataAdd { get; set; }
    }
}
