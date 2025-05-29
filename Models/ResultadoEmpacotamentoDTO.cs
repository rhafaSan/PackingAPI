namespace PackingServiceApi.Models
{
    public class ResultadoEmpacotamentoDTO
    {
        public int PedidoId { get; set; }
        public List<CaixaDTO> Caixas { get; set; } = new();
    }
}