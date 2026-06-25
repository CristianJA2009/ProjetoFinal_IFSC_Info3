namespace ProjetoFinal_IFSC_info3
{
    public class Carrinho
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Carrinho_Jogo> CarrinhoJogos { get; set; } = new List<Carrinho_Jogo>();
    }
}