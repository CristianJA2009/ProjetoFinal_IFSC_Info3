namespace MeuProjeto.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string metodo { get; set; }
        public DateTime criado_em { get; set; }
        public int compraId{ get; set; }
        public Compra Compra{ get; set; }
    }
}
