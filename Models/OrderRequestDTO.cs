using System.Text.Json.Serialization;

namespace PackingServiceApi.Models
{
    public class PedidoRequestDTO
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoDTO> Pedidos { get; set; } = new();
    }
}