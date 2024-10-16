using Microsoft.AspNetCore.Mvc;
using Web_API_Suuh.Model;
using Web_API_Suuh.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Web_API_Suuh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            try
            {
                var emprestimos = _emprestimoRepo.GetAll();

                if (emprestimos == null || !emprestimos.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum emprestimo encontrado." });
                }

                var listaComUrl = emprestimos.Select(emprestimo => new Emprestimo
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataEvolucao = emprestimo.DataEvolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // GET: api/Emprestimo/{id}
        [HttpGet("{id}")]
        public ActionResult<Emprestimo> GetById(int id)
        {
            try
            {
                var emprestimo = _emprestimoRepo.GetById(id);

                if (emprestimo == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                var emprestimoComUrl = new Emprestimo
                {
                    Id = emprestimo.Id,
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataEvolucao = emprestimo.DataEvolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro,
                };

                return Ok(emprestimoComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // POST api/<EmprestimoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] EmprestimoDto novoEmprestimo)
        {
            try
            {
                var emprestimo = new Emprestimo
                {
                    DataEmprestimo = novoEmprestimo.DataEmprestimo,
                    DataEvolucao = novoEmprestimo.DataEvolucao,
                    FkMembro = novoEmprestimo.FkMembro,
                    FkLivro = novoEmprestimo.FkLivro
                };

                _emprestimoRepo.Add(emprestimo);

                var resultado = new
                {
                    Mensagem = "Emprestimo cadastrado com sucesso!",
                    DataEmprestimo = emprestimo.DataEmprestimo,
                    DataEvolucao = emprestimo.DataEvolucao,
                    FkMembro = emprestimo.FkMembro,
                    FkLivro = emprestimo.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // PUT api/<EmprestimoController>
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EmprestimoDto emprestimoAtualizado)
        {
            try
            {
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                emprestimoExistente.DataEmprestimo = emprestimoAtualizado.DataEmprestimo;
                emprestimoExistente.DataEvolucao = emprestimoAtualizado.DataEvolucao;
                emprestimoExistente.FkMembro = emprestimoAtualizado.FkMembro;
                emprestimoExistente.FkLivro = emprestimoAtualizado.FkLivro;

                _emprestimoRepo.Update(emprestimoExistente);

                var resultado = new
                {
                    Mensagem = "Emprestimo atualizado com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataEvolucao = emprestimoExistente.DataEvolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<EmprestimoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var emprestimoExistente = _emprestimoRepo.GetById(id);

                if (emprestimoExistente == null)
                {
                    return NotFound(new { Mensagem = "Emprestimo não encontrado." });
                }

                _emprestimoRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Emprestimo excluído com sucesso!",
                    DataEmprestimo = emprestimoExistente.DataEmprestimo,
                    DataEvolucao = emprestimoExistente.DataEvolucao,
                    FkMembro = emprestimoExistente.FkMembro,
                    FkLivro = emprestimoExistente.FkLivro
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }
    }
}
