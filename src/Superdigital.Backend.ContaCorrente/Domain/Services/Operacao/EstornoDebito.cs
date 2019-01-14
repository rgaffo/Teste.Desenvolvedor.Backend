using Superdigital.Backend.ContaCorrente.Domain.Entities;
using Superdigital.Backend.ContaCorrente.Domain.Interfaces;
using Superdigital.Backend.ContaCorrente.Domain.Specifications;
using System;
using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.Domain.Services.Operacao
{
    public class EstornoDebito : IOperacao
    {
        protected readonly IContaClienteRepository _contaClienteRepository;
        protected readonly ILancamentoRepository _lancamentoRepository;
        protected IEnumerable<ISpecification<ContaCliente>> _regrasNegocio;

        public EstornoDebito(IContaClienteRepository contaClienteRepository,
            ILancamentoRepository lancamentoRepository)
        {
            _contaClienteRepository = contaClienteRepository;
            _lancamentoRepository = lancamentoRepository;

            _regrasNegocio = new List<ISpecification<ContaCliente>>()
            {
                new ContaClienteNaoEncontradaSpecification()
            };
        }

        public ICollection<string> Efetuar(int Id, double valor)
        {
            var listaErros = new List<string>();

            var conta = _contaClienteRepository.Find(Id);

            foreach (var spec in _regrasNegocio)
            {
                if (spec.IsSatisfiedBy(conta, valor))
                    listaErros.Add(spec.Mensagem);
            }

            if (listaErros.Count == 0)
            {
                conta.Saldo += valor;
                _contaClienteRepository.Update(conta);

                var lancamento = new Lancamento()
                {
                    ContaClienteId = Id,
                    TipoOperacao = TipoOperacao.EstornoDebito,
                    Valor = valor
                };

                _lancamentoRepository.Add(lancamento);
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
