namespace PackingServiceApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int Pedido_Id { get; set; }
        public List<Produto> Produtos { get; set; } = new();
        public ICollection<ResultadoEmpacotamento> ResultadosEmpacotamento { get; set; } = new List<ResultadoEmpacotamento>();

    }
}