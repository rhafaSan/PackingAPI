namespace PackingServiceApi.Models
{
    public class CaixaDTO
    {
        public string CaixaId { get; set; }
        public List<ProdutoDTO> Produtos { get; set; } = new();
        public string? Observacao { get; set; }
    }
}