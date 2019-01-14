using Superdigital.Backend.ContaCorrente.Domain.Interfaces;
using Superdigital.Backend.ContaCorrente.Domain.Services.Operacao;
using System;
using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.Domain.Services
{
    public class ContaClienteServico : IContaClienteServico
    {
        protected readonly IContaClienteRepository _contaClienteRepository;
        protected readonly ILancamentoRepository _lancamentoRepository;
        protected readonly IOperacao _operacaoCredito;
        protected readonly IOperacao _operacaoDebito;
        protected readonly IOperacao _operacaoEstorno;

        public ContaClienteServico(IContaClienteRepository contaClienteRepository,
            ILancamentoRepository lancamentoRepository)
        {
            _contaClienteRepository = contaClienteRepository;
            _lancamentoRepository = lancamentoRepository;
            _operacaoCredito = new Credito(contaClienteRepository, lancamentoRepository);
            _operacaoDebito = new Debito(contaClienteRepository, lancamentoRepository);
            _operacaoEstorno = new EstornoDebito(contaClienteRepository, lancamentoRepository);
        }

        public ICollection<string> Efetuar(int ContaOrigemId, int ContaDestinoId, double Valor)
        {
            var listaErros = new List<string>();

            if (ContaOrigemId <= 0)
                listaErros.Add("Informe a conta de origem!");

            if (ContaDestinoId <= 0)
                listaErros.Add("Informe a conta de destino!");

            if (Valor <= 0)
                listaErros.Add("Informe o valor da operação!");

            if (ContaOrigemId == ContaDestinoId)
                listaErros.Add("Conta Origem e Destino devem ser diferentes!");

            if (listaErros.Count == 0)
            {
                var debitoOK = _operacaoDebito.Efetuar(ContaOrigemId, Valor);

                if (debitoOK.Count > 0)
                    listaErros.AddRange(debitoOK);
            }

            if (listaErros.Count == 0)
            {
                var creditoOK = _operacaoCredito.Efetuar(ContaDestinoId, Valor);

                if (creditoOK.Count > 0)
                {
                    listaErros.AddRange(creditoOK);
                    _operacaoEstorno.Efetuar(ContaOrigemId, Valor);
                }
                else
                {
                    listaErros.Add("Operação concluída!");
                }
            }

            return listaErros;
        }

        public void Dispose()
        {
            _contaClienteRepository.Dispose();
            _lancamentoRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}