namespace PackingServiceApi.Models
{
    public class CaixaEmbalagem
    {
        public int Id { get; set; }
        public string CaixaId { get; set; }
        public string? Observacao { get; set; }

        public int ResultadoEmpacotamentoId { get; set; }
        public ResultadoEmpacotamento ResultadoEmpacotamento { get; set; }

        public ICollection<ProdutoCaixa> Produtos { get; set; } = new List<ProdutoCaixa>();
    }
}