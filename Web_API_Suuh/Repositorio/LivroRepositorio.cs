using Web_API_Suuh.Model;
using Web_API_Suuh.ORM;

namespace Web_API_Suuh.Repositorio
{
    public class LivroRepositorio
    {
        private readonly BancoBibliotecaContext _context;

        public LivroRepositorio(BancoBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Livro livro)
        {

            // Cria uma nova entidade do tipo TbLivro a partir do objeto Livro recebido
            var tbLivro = new TbLivro()
            {
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                AnoPublicacao = livro.AnoPublicacao,
                FkCategoria = livro.FkCategoria,
                Disponibilidade = livro.Disponibilidade
            };

            // Adiciona a entidade ao contexto
            _context.TbLivros.Add(tbLivro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbLivro = _context.TbLivros.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbLivro != null)
            {
                // Remove a entidade do contexto
                _context.TbLivros.Remove(tbLivro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Livro não encontrado.");
            }
        }

        public List<Livro> GetAll()
        {
            List<Livro> listFun = new List<Livro>();

            var listTb = _context.TbLivros.ToList();

            foreach (var item in listTb)
            {
                var livro = new Livro
                {
                    Id = item.Id,
                    Titulo = item.Titulo,
                    Autor = item.Autor,
                    AnoPublicacao = item.AnoPublicacao,
                    FkCategoria = item.FkCategoria,
                    Disponibilidade = item.Disponibilidade,
                };

                listFun.Add(livro);
            }

            return listFun;
        }

        public Livro GetById(int id)
        {
            // Busca o livro pelo ID no banco de dados
            var item = _context.TbLivros.FirstOrDefault(f => f.Id == id);

            // Verifica se o livro foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Livro
            var livro = new Livro
            {
                Id = item.Id,
                Titulo = item.Titulo,
                Autor = item.Autor,
                AnoPublicacao = item.AnoPublicacao,
                FkCategoria = item.FkCategoria,
                Disponibilidade= item.Disponibilidade,
            };

            return livro; // Retorna o livro encontrado
        }

        public void Update(Livro livro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbLivro = _context.TbLivros.FirstOrDefault(f => f.Id == livro.Id);

            // Verifica se a entidade foi encontrada
            if (tbLivro != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Livro recebido
                tbLivro.Titulo = livro.Titulo;
                tbLivro.Autor = livro.Autor;
                tbLivro.AnoPublicacao = livro.AnoPublicacao;
                tbLivro.FkCategoria = livro.FkCategoria;
                tbLivro.Disponibilidade = livro.Disponibilidade;

                // Atualiza as informações no contexto
                _context.TbLivros.Update(tbLivro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Livro não encontrado.");
            }
        }

    }
}
