using Microsoft.Data.SqlClient;

namespace MeuProjeto.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha{ get; set; }
        public string? foto { get; set; }
        public int pontos { get; set; }
        public DateTime criado_em { get; set; }
        public string tipo { get; set; }
        public List<UsuarioJogo> UsuarioJogos { get; set; } = [];
        public List<Jogo> Jogos { get; set; } = [];
        public ICollection<Compra> Compras { get; } = new List<Compra>();
        public ICollection<Carrinho> Carrinhos { get; } = new List<Carrinho>();
    }
}  
