namespace PackingServiceApi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string ProdutoId { get; set; }
        public Dimensao Dimensao { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int? CaixaEmbalagemId { get; set; }
        public CaixaEmbalagem CaixaEmbalagem { get; set; }
    }
}