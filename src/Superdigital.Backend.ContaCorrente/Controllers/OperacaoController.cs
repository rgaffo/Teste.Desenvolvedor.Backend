using Microsoft.AspNetCore.Mvc;
using Superdigital.Backend.ContaCorrente.AppService;
using Superdigital.Backend.ContaCorrente.Model;
using Superdigital.Backend.ContaCorrente.Security;

namespace Superdigital.Backend.ContaCorrente.Controllers
{
    [Login("superdigital.com.br")]
    [Route("api/[controller]")]
    [ApiController]
    public class OperacaoController : ControllerBase
    {
        protected readonly IOperacaoServico _operacaoServico;

        public OperacaoController(IOperacaoServico operacaoService)
        {
            _operacaoServico = operacaoService;
        }

        // GET: api/Operacao
        [HttpGet]
        public string Get()
        {
            return string.Format("Faça um post com os dados no formato '{0}' para executar a operação de débito e crédito", "{contaOrigemId:IdContaOrigem,contaDestinoId:IdContaDestino,valor:Valor}");
        }

        // POST: api/Operacao
        [HttpPost]
        public ActionResult Post([FromBody] DadosConta dadosConta)
        {
            if (dadosConta == null)
                return BadRequest();

            return new ObjectResult(_operacaoServico.Efetuar(dadosConta));

            // Body = {"contaOrigemId":1,"contaDestinoId":2,"valor":100}
        }
    }
}
