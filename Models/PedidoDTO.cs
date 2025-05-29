using System.Text.Json.Serialization;

namespace PackingServiceApi.Models
{
    public class PedidoDTO
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("produtos")]
        public List<ProdutoDTO> Produtos { get; set; }
    }
}