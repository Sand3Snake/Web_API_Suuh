using Microsoft.AspNetCore.Mvc;
using Web_API_Suuh.Model;
using Web_API_Suuh.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Suuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoRepositorio _emprestimoRepo;

        public EmprestimoController(EmprestimoRepositorio emprestimoRepo)
        {
            _emprestimoRepo = emprestimoRepo;
        }

        // GET: api/Emprestimo
        [HttpGet]
        public ActionResult<List<Emprestimo>> GetAll()
        {
            // Chama o repositório para obter todos os emprestimos
            var emprestimos = _emprestimoRepo.GetAll();

            // Verifica se a lista de emprestimos está vazia
            if (emprestimos == null || !emprestimos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum emprestimo encontrado." });
            }

            // Mapeia a lista de emprestimos para incluir a URL da foto
            var listaComUrl = emprestimos.Select(emprestimo => new Emprestimo
            {
                Id = emprestimo.Id,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataEvolucao = emprestimo.DataEvolucao,
                FkMembro = emprestimo.FkMembro,
                FkLivro = emprestimo.FkLivro,

            });

            // Retorna a lista de emprestimos com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Emprestimo/{id}
        [HttpGet("{id}")]
        public ActionResult<Emprestimo> GetById(int id)
        {
            // Chama o repositório para obter o emprestimo pelo ID
            var emprestimo = _emprestimoRepo.GetById(id);

            // Se o emprestimo não for encontrado, retorna uma resposta 404
            if (emprestimo == null)
            {
                return NotFound(new { Mensagem = "Emprestimo não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o emprestimo encontrado para incluir a URL da foto
            var emprestimoComUrl = new Emprestimo
            {
                Id = emprestimo.Id,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataEvolucao = emprestimo.DataEvolucao,
                FkMembro = emprestimo.FkMembro,
                FkLivro= emprestimo.FkLivro,
            };

            // Retorna a categoria com status 200 OK
            return Ok(emprestimoComUrl);
        }

        // POST api/<EmprestimoController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] EmprestimoDto novoEmprestimo)
        {
            // Cria uma nova instância do modelo Emprestimo a partir do DTO recebido
            var emprestimo = new Emprestimo
            {
                DataEmprestimo = novoEmprestimo.DataEmprestimo,
                DataEvolucao = novoEmprestimo.DataEvolucao,
                FkMembro = novoEmprestimo.FkMembro,
                FkLivro = novoEmprestimo.FkLivro

            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _emprestimoRepo.Add(emprestimo);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo cadastrado com sucesso!",
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataEvolucao = emprestimo.DataEvolucao,
                FkMembro = emprestimo.FkMembro,
                FkLivro = emprestimo.FkLivro

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<EmprestimoController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EmprestimoDto emprestimoAtualizado)
        {
            // Busca o emprestimo existente pelo Id
            var emprestimoExistente = _emprestimoRepo.GetById(id);

            // Verifica se o emprestimo foi encontrado
            if (emprestimoExistente == null)
            {
                return NotFound(new { Mensagem = "Emprestimo não encontrado." });
            }

            // Atualiza os dados do emprestimo existente com os valores do objeto recebido
            emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
            emprestimoExistente.DataEvolucao = emprestimoAtualizado.DataEvolucao;
            emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
            emprestimoExistente.FkLivro = emprestimoAtualizado.FkLivro;


            // Chama o método de atualização do repositório, passando a nova foto
            _emprestimoRepo.Update(emprestimoExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo atualizado com sucesso!",
                DataEmprestimo = emprestimoExistente.DataEmprestimo,
                DataEvolucao = emprestimoExistente.DataEvolucao,
                FkMembro = emprestimoExistente.FkMembro,
                FkLivro = emprestimoExistente.FkLivro

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<EmprestimoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o emprestimo existente pelo Id
            var emprestimoExistente = _emprestimoRepo.GetById(id);

            // Verifica se o emprestimo foi encontrado
            if (emprestimoExistente == null)
            {
                return NotFound(new { Mensagem = "Emprestimo não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _emprestimoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Emprestimo excluído com sucesso!",
                DataEmprestimo = emprestimoExistente.DataEmprestimo,
                DataEvolucao = emprestimoExistente.DataEvolucao,
                FkMembro = emprestimoExistente.FkMembro,
                FkLivro = emprestimoExistente.FkLivro

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }
}
