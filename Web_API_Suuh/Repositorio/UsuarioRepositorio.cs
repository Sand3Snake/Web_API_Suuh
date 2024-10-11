using Web_API_Suuh.Model;
using Web_API_Suuh.ORM;

namespace Web_API_Suuh.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly BancoBibliotecaContext _context;

        public UsuarioRepositorio(BancoBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {

            // Cria uma nova entidade do tipo TbUsuario a partir do objeto Usuario recebido
            var tbUsuario = new TbUsuario()
            {
                Usuario = usuario.Usuario1,
                Senha = usuario.Senha,
                
            };

            // Adiciona a entidade ao contexto
            _context.TbUsuarios.Add(tbUsuario);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
    }
}
