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
            try
            {
                var funcionarios = _funcionarioRepo.GetAll();

                if (funcionarios == null || !funcionarios.Any())
                {
                    return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
                }

                var listaComUrl = funcionarios.Select(funcionario => new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo,
                }).ToList();

                return Ok(listaComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetById(int id)
        {
            try
            {
                var funcionario = _funcionarioRepo.GetById(id);

                if (funcionario == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." });
                }

                var funcionarioComUrl = new Funcionario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo,
                };

                return Ok(funcionarioComUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // POST api/<FuncionarioController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] FuncionarioDto novoFuncionario)
        {
            try
            {
                var funcionario = new Funcionario
                {
                    Nome = novoFuncionario.Nome,
                    Email = novoFuncionario.Email,
                    Telefone = novoFuncionario.Telefone,
                    Cargo = novoFuncionario.Cargo,
                };

                _funcionarioRepo.Add(funcionario);

                var resultado = new
                {
                    Mensagem = "Usuário cadastrado com sucesso!",
                    Nome = funcionario.Nome,
                    Email = funcionario.Email,
                    Telefone = funcionario.Telefone,
                    Cargo = funcionario.Cargo,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // PUT api/<FuncionarioController>
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] FuncionarioDto funcionarioAtualizado)
        {
            try
            {
                var funcionarioExistente = _funcionarioRepo.GetById(id);

                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." });
                }

                funcionarioExistente.Nome = funcionarioAtualizado.Nome;
                funcionarioExistente.Email = funcionarioAtualizado.Email;
                funcionarioExistente.Telefone = funcionarioAtualizado.Telefone;
                funcionarioExistente.Cargo = funcionarioAtualizado.Cargo;

                _funcionarioRepo.Update(funcionarioExistente);

                var resultado = new
                {
                    Mensagem = "Usuário atualizado com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Email = funcionarioExistente.Email,
                    Telefone = funcionarioExistente.Telefone,
                    Cargo = funcionarioExistente.Cargo,
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno do servidor.", Detalhes = ex.Message });
            }
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var funcionarioExistente = _funcionarioRepo.GetById(id);

                if (funcionarioExistente == null)
                {
                    return NotFound(new { Mensagem = "Funcionário não encontrado." });
                }

                _funcionarioRepo.Delete(id);

                var resultado = new
                {
                    Mensagem = "Usuário excluído com sucesso!",
                    Nome = funcionarioExistente.Nome,
                    Email = funcionarioExistente.Email,
                    Telefone = funcionarioExistente.Telefone,
                    Cargo = funcionarioExistente.Cargo,
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
