namespace PackingServiceApi.Models
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Caixa_Id { get; set; } = string.Empty;
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
    }
}