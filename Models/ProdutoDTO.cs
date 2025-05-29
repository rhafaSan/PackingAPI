using System.Text.Json.Serialization;

namespace PackingServiceApi.Models
{
    public class ProdutoDTO
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; }
        [JsonPropertyName("dimensoes")]
        public DimensaoDTO Dimensoes { get; set; }
    }
}