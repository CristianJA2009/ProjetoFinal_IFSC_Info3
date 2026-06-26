namespace MeuProjeto.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<CarrinhoJogo> CarrinhoJogos { get; set; } = [];
        public List<Jogo> Jogos { get; set; } = [];
    }
}
