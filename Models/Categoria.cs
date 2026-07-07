namespace MeuProjeto.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public ICollection<Jogo> Jogos { get; } = new List<Jogo>();
    }
}
