using System.ComponentModel.DataAnnotations.Schema;

namespace PackingServiceApi.Models
{
    public class ProdutoCaixa
    {
        public int Id { get; set; }
        public string ProdutoId { get; set; }
        [NotMapped]
        public Dimensao Dimensao { get; set; }

        public int CaixaEmbalagemId { get; set; }
        public CaixaEmbalagem CaixaEmbalagem { get; set; }
    }
}