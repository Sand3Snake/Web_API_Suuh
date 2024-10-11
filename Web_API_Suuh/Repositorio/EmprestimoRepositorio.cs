using Web_API_Suuh.Model;
using Web_API_Suuh.ORM;

namespace Web_API_Suuh.Repositorio
{
    public class EmprestimoRepositorio
    {
        private readonly BancoBibliotecaContext _context;

        public EmprestimoRepositorio(BancoBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Emprestimo emprestimo)
        {

            // Cria uma nova entidade do tipo TbEmprestimo a partir do objeto Emprestimo recebido
            var tbEmprestimo = new TbEmprestimo()
            {
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataEvolucao = emprestimo.DataEvolucao,
                FkMembro = emprestimo.FkMembro,
                FkLivro = emprestimo.FkLivro,
            };

            // Adiciona a entidade ao contexto
            _context.TbEmprestimos.Add(tbEmprestimo);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Remove a entidade do contexto
                _context.TbEmprestimos.Remove(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }

        public List<Emprestimo> GetAll()
        {
            List<Emprestimo> listFun = new List<Emprestimo>();

            var listTb = _context.TbEmprestimos.ToList();

            foreach (var item in listTb)
            {
                var emprestimo = new Emprestimo
                {
                    Id = item.Id,
                    DataEmprestimo = item.DataEmprestimo,
                    DataEvolucao = item.DataEvolucao,
                    FkMembro = item.FkMembro,
                    FkLivro = item.FkLivro,

                };

                listFun.Add(emprestimo);
            }

            return listFun;
        }

        public Emprestimo GetById(int id)
        {
            // Busca o emprestimo pelo ID no banco de dados
            var item = _context.TbEmprestimos.FirstOrDefault(f => f.Id == id);

            // Verifica se o emprestimo foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Emprestimo
            var emprestimo = new Emprestimo
            {
                Id = item.Id,
                DataEmprestimo = item.DataEmprestimo,
                DataEvolucao = item.DataEvolucao,
                FkMembro= item.FkMembro,
                FkLivro= item.FkLivro,
            };

            return emprestimo; // Retorna o emprestimo encontrado
        }

        public void Update(Emprestimo emprestimo)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEmprestimo = _context.TbEmprestimos.FirstOrDefault(f => f.Id == emprestimo.Id);

            // Verifica se a entidade foi encontrada
            if (tbEmprestimo != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Emprestimo recebido
                tbEmprestimo.DataEmprestimo= emprestimo.DataEmprestimo;
                tbEmprestimo.DataEvolucao = emprestimo.DataEvolucao;
                tbEmprestimo.FkMembro= emprestimo.FkMembro;
                tbEmprestimo.FkLivro= emprestimo.FkLivro;

                // Atualiza as informações no contexto
                _context.TbEmprestimos.Update(tbEmprestimo);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Emprestimo não encontrado.");
            }
        }
    }
}
