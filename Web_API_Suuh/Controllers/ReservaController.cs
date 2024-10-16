using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_Suuh.Model;
using Web_API_Suuh.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Suuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaRepositorio _reservaRepo;

        public ReservaController(ReservaRepositorio reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        // GET: api/Reserva
        [HttpGet]
        public ActionResult<List<Reserva>> GetAll()
        {
            try
            {
                var reservas = _reservaRepo.GetAll();

                if (reservas == null || !reservas.Any())
                {
                    return NotFound(new { Mensagem = "Nenhuma reserva encontrada." });
                }

                var listaComUrl = reservas.Select(reserva => new Reserva
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro,
                   
                });

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar as reservas.", Erro = ex.Message });
            }
        }

        // GET: api/Reserva/{id}
        [HttpGet("{id}")]
        public ActionResult<Reserva> GetById(int id)
        {
            try
            {
                var reserva = _reservaRepo.GetById(id);

                if (reserva == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                var reservaComUrl = new Reserva
                {
                    Id = reserva.Id,
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro,
                   
                };

                return Ok(reservaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao buscar a reserva.", Erro = ex.Message });
            }
        }

        // POST api/<ReservaController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ReservaDto novaReserva)
        {
            try
            {
                var reserva = new Reserva
                {
                    DataReserva = novaReserva.DataReserva,
                    FkMembro = novaReserva.FkMembro,
                    FkLivro = novaReserva.FkLivro,
                    
                };

                _reservaRepo.Add(reserva);

                var resultado = new
                {
                    Mensagem = "Reserva cadastrada com sucesso!",
                    DataReserva = reserva.DataReserva,
                    FkMembro = reserva.FkMembro,
                    FkLivro = reserva.FkLivro,
                   
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao cadastrar a reserva.", Erro = ex.Message });
            }
        }

        // PUT api/<ReservaController>
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ReservaDto reservaAtualizado)
        {
            try
            {
                var reservaExistente = _reservaRepo.GetById(id);

                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                reservaExistente.DataReserva = reservaAtualizado.DataReserva;
                reservaExistente.FkMembro = reservaAtualizado.FkMembro;
                reservaExistente.FkLivro = reservaAtualizado.FkLivro;
               

                _reservaRepo.Update(reservaExistente);

                var resultado = new
                {
                    Mensagem = "Reserva atualizada com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro,
                   
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao atualizar a reserva.", Erro = ex.Message });
            }
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var reservaExistente = _reservaRepo.GetById(id);

                if (reservaExistente == null)
                {
                    return NotFound(new { Mensagem = "Reserva não encontrada." });
                }

                _reservaRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Reserva excluído com sucesso!",
                    DataReserva = reservaExistente.DataReserva,
                    FkMembro = reservaExistente.FkMembro,
                    FkLivro = reservaExistente.FkLivro,
                   
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Ocorreu um erro ao excluir a reserva.", Erro = ex.Message });
            }
        }
    }
}
