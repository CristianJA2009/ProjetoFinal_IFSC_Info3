namespace ProjetoFinal_IFSC_info3
{
    public class Pagamento
    {
        public int id { get; set; }
        public string metodo { get; set; }
        public DateTime pago_em{ get; set; }
        public int compra_id { get; set; }
        public Compra Compra { get; set; }
    }
}