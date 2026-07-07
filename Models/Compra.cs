namespace MeuProjeto.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public float valor_total { get; set; }
        public DateTime criado_em { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Pagamento Pagamento { get; set; }
        public List<CompraJogo> CompraJogos { get; set; } = [];
        public List<Jogo> Jogos { get; set; } = [];
    }
}
