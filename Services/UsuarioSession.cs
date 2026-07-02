namespace MeuProjeto.Services
{
    public class UsuarioSessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioSessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Nome =>
            _httpContextAccessor.HttpContext?.Session.GetString("UsuarioName");

        public string? Tipo =>
            _httpContextAccessor.HttpContext?.Session.GetString("UsuarioTipo");

        public int? Id
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.Session.GetString("UsuarioId");
                return int.TryParse(id, out var valor) ? valor : null;
            }
        }

        public void EncerrarSessao()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
    }
}
