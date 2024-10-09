using Microsoft.AspNetCore.Mvc;
using Web_API_Suuh.Model;
using Web_API_Suuh.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Suuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepositorio _funcionarioRepo;

        public FuncionarioController(FuncionarioRepositorio funcionarioRepo)
        {
            _funcionarioRepo = funcionarioRepo;
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Funcionario>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var funcionarios = _funcionarioRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (funcionarios == null || !funcionarios.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = funcionarios.Select(funcionario => new Funcionario
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                Cargo = funcionario.Cargo,
            });

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var funcionario = _funcionarioRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (funcionario == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var funcionarioComUrl = new Funcionario
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone= funcionario.Telefone,
                Cargo = funcionario.Cargo,
                
            };

            // Retorna o funcionário com status 200 OK
            return Ok(funcionarioComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var funcionario = new Funcionario
            {
                Nome = novoFuncionario.Nome,
                Email = novoFuncionario.Email,
                Telefone = novoFuncionario.Telefone,
                Cargo= novoFuncionario.Cargo,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _funcionarioRepo.Add(funcionario);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = funcionario.Nome,
                Email = funcionario.Email,
                Telefone = funcionario.Telefone,
                Cargo = funcionario.Cargo,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            funcionarioExistente.Nome = funcionarioAtualizado.Nome;
            funcionarioExistente.Email = funcionarioAtualizado.Email;
            funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
            funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;

            // Chama o método de atualização do repositório, passando a nova foto
            _funcionarioRepo.Update(funcionarioExistente);

          
            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = funcionarioExistente.Nome,
                Email = funcionarioExistente.Email,
                Telefone = funcionarioExistente.Telefone,
                Cargo = funcionarioExistente.Cargo,
                
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var funcionarioExistente = _funcionarioRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (funcionarioExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _funcionarioRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = funcionarioExistente.Nome,
                Email = funcionarioExistente.Email,
                Telefone = funcionarioExistente.Telefone,
                Cargo = funcionarioExistente.Cargo,

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
