using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain;
using WebApi.Repository;
using Newtonsoft.Json;
using System.Text.Json;

namespace WebApi.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly ILogger<ContatoController> _logger;
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(ILogger<ContatoController> logger, IContatoRepository contatoRepository)
        {
            _logger = logger;
            _contatoRepository = contatoRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                IEnumerable<Contato> contatos = _contatoRepository.Listar();

                return Ok(contatos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao listar");
                return new StatusCodeResult(500);
            }


        }

        [HttpGet("{id}")]
        public IActionResult Selecionar(string id)
        {
            try
            {
                var contato = _contatoRepository.Selecionar(id);
                if (contato == null)
                {
                    return NotFound();
                }
                var contact = ToContatoModel(contato);
                return Ok(contact);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao listar");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult Persistir(Contato contato)
        {
            try
            {
                //Contato contato = JsonConvert.DeserializeObject<Contato>(json);
                contato.Id = Guid.NewGuid().ToString();
                _contatoRepository.Persistir(contato);
                return Ok(contato);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao listar");
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public IActionResult Atualizar(Contato contato)//
        {
            try
            {
                //var contato = JsonConvert.DeserializeObject<Contato>(json);
                _contatoRepository.Atualizar(contato);
                var _contato = _contatoRepository.Selecionar(contato.Id);
                if (_contato == null)
                {
                    return NotFound();
                }
                var contact = ToContatoModel(_contato);
                return Ok(contact);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao atualizar");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(string id)
        {
            try
            {
                _contatoRepository.Excluir(id);
                return Ok("Contato com o ID " + id + " excluido com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao listar");
                return new StatusCodeResult(500);
            }
        }

        private Contato ToContatoModel(Contato contato)
        {
            return new Contato
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Telefone = contato.Telefone,
                Email = contato.Email
            };
        }
    }
}
