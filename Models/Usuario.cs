namespace ProjetoFinal_IFSC_info3
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string foto { get; set; }
        public int pontos { get; set; }
        public DateTime criado_em { get; set; }
        public string tipo { get; set; }
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();
        public Carrinho Carrinho { get; set; }
        public ICollection<Usuario_Jogo> UsuarioJogos { get; set; } = new List<Usuario_Jogo>();
    }
}