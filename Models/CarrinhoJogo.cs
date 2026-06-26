namespace MeuProjeto.Models
{
    public class CarrinhoJogo
    {
        public int usuarioId { get; set; }
        public int jogoId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public Jogo Jogo { get; set; } = null!;
    }
}
