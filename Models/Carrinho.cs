namespace MeuProjeto.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }
    }
}
