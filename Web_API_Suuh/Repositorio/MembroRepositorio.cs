using Web_API_Suuh.Model;
using Web_API_Suuh.ORM;

namespace Web_API_Suuh.Repositorio
{
    public class MembroRepositorio
    {
        private readonly BancoBibliotecaContext _context;

        public MembroRepositorio(BancoBibliotecaContext context)
        {
            _context = context;
        }

        public void Add(Membro membro)
        {

            // Cria uma nova entidade do tipo TbMembro a partir do objeto Membro recebido
            var tbMembro = new TbMembro()
            {
                Nome = membro.Nome,
                Email= membro.Email,
                Telefone = membro.Telefone,
                DataCadastro = membro.DataCadastro,
                TipoMembro = membro.TipoMembro
            };

            // Adiciona a entidade ao contexto
            _context.TbMembros.Add(tbMembro);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Remove a entidade do contexto
                _context.TbMembros.Remove(tbMembro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }

        public List<Membro> GetAll()
        {
            List<Membro> listFun = new List<Membro>();

            var listTb = _context.TbMembros.ToList();

            foreach (var item in listTb)
            {
                var membro = new Membro
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    DataCadastro = item.DataCadastro,
                    TipoMembro = item.TipoMembro,
                };

                listFun.Add(membro);
            }

            return listFun;
        }

        public Membro GetById(int id)
        {
            // Busca o membro pelo ID no banco de dados
            var item = _context.TbMembros.FirstOrDefault(f => f.Id == id);

            // Verifica se o membro foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Membro
            var membro = new Membro
            {
                Id = item.Id,
                Nome = item.Nome,
                Email = item.Email,
                Telefone = item.Telefone,
                DataCadastro = item.DataCadastro,
                TipoMembro = item.TipoMembro,
            };

            return membro; // Retorna o membro encontrado
        }

        public void Update(Membro membro)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbMembro = _context.TbMembros.FirstOrDefault(f => f.Id == membro.Id);

            // Verifica se a entidade foi encontrada
            if (tbMembro != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Membro recebido
                tbMembro.Nome = membro.Nome;
                tbMembro.Email = membro.Email;
                tbMembro.Telefone = membro.Telefone;
                tbMembro.DataCadastro = membro.DataCadastro;
                tbMembro.TipoMembro = membro.TipoMembro;

                // Atualiza as informações no contexto
                _context.TbMembros.Update(tbMembro);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Membro não encontrado.");
            }
        }
    }
}
