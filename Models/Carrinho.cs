namespace ProjetoFinal_IFSC_info3
{
    public class Carrinho
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
    }
}