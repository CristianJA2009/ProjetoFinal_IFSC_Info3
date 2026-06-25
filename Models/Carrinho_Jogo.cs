namespace ProjetoFinal_IFSC_info3
{
    public class Carrinho_Jogo
    {
        public int id { get; set; }
        public int carrinho_id { get; set; }
        public int jogo_id { get; set; }
        public int qtd { get; set; }
        public Carrinho Carrinho { get; set; }
        public Jogo Jogo { get; set; }
    }
}