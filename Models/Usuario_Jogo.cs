namespace ProjetoFinal_IFSC_info3
{
    public class Usuario_Jogo
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int jogo_id { get; set; }
        public string chave_ativacao { get; set; }
        public DateTime adquirido_em { get; set; }
        public Usuario Usuario { get; set; }
        public Jogo Jogo { get; set; }
    }
}