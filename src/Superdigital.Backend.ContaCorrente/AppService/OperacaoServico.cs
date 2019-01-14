using Superdigital.Backend.ContaCorrente.Domain.Interfaces;
using Superdigital.Backend.ContaCorrente.Model;
using System;
using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.AppService
{
    public class OperacaoServico : IOperacaoServico
    {
        protected readonly IContaClienteServico _contaClienteService;

        public OperacaoServico(IContaClienteServico contaClienteService)
        {
            _contaClienteService = contaClienteService;
        }

        public ICollection<string> Efetuar(DadosConta dadosConta)
        {
            return _contaClienteService.Efetuar(dadosConta.ContaOrigemId, dadosConta.ContaDestinoId, dadosConta.Valor);
        }

        public void Dispose()
        {
            _contaClienteService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}