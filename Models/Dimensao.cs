namespace PackingServiceApi.Models
{
    public class Dimensao
    {
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }

        public double Volume => Altura * Largura * Comprimento;
    }
}