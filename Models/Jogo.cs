namespace ProjetoFinal_IFSC_info3
{
    public class Jogo
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public float valor { get; set; }
        public string capa { get; set; }
        public string banner { get; set; }
        public DateTime criado_em { get; set; }
        public int categoria_id { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}