namespace ProjetoFinal_IFSC_info3
{
    public class Compra_Jogo
    {
        public int id { get; set; }
        public int compra_id { get; set; }
        public int jogo_id { get; set; }
        public float preco_pago { get; set; }
        public Compra Compra { get; set; }
        public Jogo Jogo { get; set; }
    }
}