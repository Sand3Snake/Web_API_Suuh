using Microsoft.AspNetCore.Mvc;
using Web_API_Suuh.Model;
using Web_API_Suuh.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Suuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepositorio _categoriaRepo;

        public CategoriaController(CategoriaRepositorio categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        // GET: api/Categoria
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            // Chama o repositório para obter todas as categorias
            var categorias = _categoriaRepo.GetAll();

            // Verifica se a lista de categorias está vazia
            if (categorias == null || !categorias.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma categoria encontrada." });
            }

            // Mapeia a lista de categorias para incluir a URL da foto
            var listaComUrl = categorias.Select(categoria => new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                
            });

            // Retorna a lista de categorias com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Categoria/{id}
        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            // Chama o repositório para obter o categoria pelo ID
            var categoria = _categoriaRepo.GetById(id);

            // Se a categoria não for encontrada, retorna uma resposta 404
            if (categoria == null)
            {
                return NotFound(new { Mensagem = "Categoria não encontrada." }); // Retorna 404 com mensagem
            }

            // Mapeia a categoria encontrada para incluir a URL da foto
            var categoriaComUrl = new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                

            };

            // Retorna a categoria com status 200 OK
            return Ok(categoriaComUrl);
        }

        // POST api/<CategoriaController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] CategoriaDto novaCategoria)
        {
            // Cria uma nova instância do modelo Categoria a partir do DTO recebido
            var categoria = new Categoria
            {
                Nome = novaCategoria.Nome,
                Descricao = novaCategoria.Descricao,
                
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _categoriaRepo.Add(categoria);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Categoria cadastrada com sucesso!",
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
               
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<CategoriaController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] CategoriaDto categoriaAtualizado)
        {
            // Busca a categoria existente pelo Id
            var categoriaExistente = _categoriaRepo.GetById(id);

            // Verifica se a categoria foi encontrada
            if (categoriaExistente == null)
            {
                return NotFound(new { Mensagem = "Categoria não encontrada." });
            }

            // Atualiza os dados da categoria existente com os valores do objeto recebido
            categoriaExistente.Nome = categoriaAtualizado.Nome;
            categoriaExistente.Descricao = categoriaAtualizado.Descricao;


            // Chama o método de atualização do repositório, passando a nova foto
            _categoriaRepo.Update(categoriaExistente);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Categoria atualizada com sucesso!",
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao,
                
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<CateforiaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o categoria existente pelo Id
            var categoriaExistente = _categoriaRepo.GetById(id);

            // Verifica se a categoria foi encontrado
            if (categoriaExistente == null)
            {
                return NotFound(new { Mensagem = "Categoria não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _categoriaRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Categoria excluída com sucesso!",
                Nome = categoriaExistente.Nome,
                Descricao = categoriaExistente.Descricao,
               
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
