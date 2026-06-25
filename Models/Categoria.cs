namespace ProjetoFinal_IFSC_info3
{
    public class Categoria
    {
        public int id { get; set; }
        public string nome { get; set; }
        public ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
    }
}