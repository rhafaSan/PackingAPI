namespace PackingServiceApi.Models
{
    public class CaixaEmpacotada
    {
        public int Id { get; set; }
        public string Caixa_Id { get; set; } = string.Empty;

        public string Observacao { get; set; } = string.Empty;
        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; } = null!;

        public ICollection<Produto> Produtos { get; set; } = [];
    }
}