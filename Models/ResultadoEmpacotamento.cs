namespace PackingServiceApi.Models
{
    public class ResultadoEmpacotamento
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public ICollection<CaixaEmbalagem> Caixas { get; set; } = new List<CaixaEmbalagem>();


    }
}