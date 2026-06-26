namespace MeuProjeto.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public float valor { get; set; }
        public string capa { get; set; }
        public string banner { get; set; }
        public DateTime criado_em { get; set; }
        public int categoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public List<UsuarioJogo> UsuarioJogos { get; set; } = [];
        public List<Usuario> Usuarios { get; set; } = [];
        public List<CarrinhoJogo> CarrinhoJogos { get; set; } = [];
        public List<Carrinho> Carrinhos { get; set; } = [];
        public List<CompraJogo> CompraJogos { get; set; } = [];
        public List<Compra> Compras { get; set; } = [];
    }
}