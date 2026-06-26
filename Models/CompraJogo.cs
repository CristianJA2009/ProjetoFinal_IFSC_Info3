namespace MeuProjeto.Models
{
    public class CompraJogo
    {
        public int compraId { get; set; }
        public int jogoId { get; set; }
        public Compra Compra { get; set; } = null!;
        public Jogo Jogo { get; set; } = null!;
    }
}
