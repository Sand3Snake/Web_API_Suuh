using Microsoft.EntityFrameworkCore;

using Web_API_Suuh.ORM;

namespace ProjetoWebAPI.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly BancoBibliotecaContext _context;

        public UsuarioRepositorio(BancoBibliotecaContext context)
        {
            _context = context;
        }

        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuarios.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

        // Você pode adicionar métodos adicionais para gerenciar usuários
    }
}
