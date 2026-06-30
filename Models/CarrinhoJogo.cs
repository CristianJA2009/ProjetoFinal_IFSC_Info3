namespace MeuProjeto.Models
{
    public class CarrinhoJogo
    {
        public int carrinhoId { get; set; }
        public int jogoId { get; set; }
        public Carrinho Carrinho { get; set; } = null!;
        public Jogo Jogo { get; set; } = null!;
    }
}
