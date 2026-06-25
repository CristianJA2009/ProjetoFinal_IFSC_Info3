namespace ProjetoFinal_IFSC_info3
{
    public class Compra
    {
        public int id { get; set; }
        public float valor_total{ get; set; }
        public DateTime criado_em { get; set; }
        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; }
        public Pagamento Pagamento { get; set; }
        public ICollection<Compra_Jogo> CompraJogos{ get; set; } = new List<Compra_Jogo>();
    }
}