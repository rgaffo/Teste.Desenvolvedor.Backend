using Microsoft.EntityFrameworkCore;
using Superdigital.Backend.ContaCorrente.Data.Context;
using Superdigital.Backend.ContaCorrente.Data.Repositories;
using Superdigital.Backend.ContaCorrente.Domain.Interfaces;
using Superdigital.Backend.ContaCorrente.Domain.Services;
using System.Linq;
using Xunit;

namespace Superdigital.Backend.Tests
{
    public class ContaClienteTests
    {
        private ContaCorrenteContext contaCorrenteContext;
        private IContaClienteRepository _contaClienteRepository;
        private ILancamentoRepository _lancamentoRepository;
        private IContaClienteServico _contaClienteServico;

        public ContaClienteTests()
        {
            var conn = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ContaCorrenteDb; Integrated Security = True";
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(conn);

            contaCorrenteContext = new ContaCorrenteContext(options.Options);

            _contaClienteRepository = new ContaClienteRepository(contaCorrenteContext);
            _lancamentoRepository = new LancamentoRepository(contaCorrenteContext);
            _contaClienteServico = new ContaClienteServico(_contaClienteRepository, _lancamentoRepository);
        }

        [Fact]
        public void TodosParametrosInvalidos()
        {
            var conta = _contaClienteServico.Efetuar(0, 0, 0);

            int ret = conta.Where(c => c == "Informe a conta de origem!").Count();
            int ret1 = conta.Where(c => c == "Informe a conta de destino!").Count();
            int ret2 = conta.Where(c => c == "Informe o valor da operação!").Count();
            int ret3 = conta.Where(c => c == "Conta Origem e Destino devem ser diferentes!").Count();

            int total = ret + ret1 + ret2 + ret3;

            Assert.Equal(4, total);
        }

        [Fact]
        public void ContaOrigemZerada()
        {
            var conta = _contaClienteServico.Efetuar(0, 1, 1);

            int ret = conta.Where(c => c == "Informe a conta de origem!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void ContaDestinoZerada()
        {
            var conta = _contaClienteServico.Efetuar(1, 0, 1);

            int ret = conta.Where(c => c == "Informe a conta de destino!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void ValorOperacaoZerado()
        {
            var conta = _contaClienteServico.Efetuar(1, 1, 0);

            int ret = conta.Where(c => c == "Informe o valor da operação!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void ContaOrigemDestinoIguais()
        {
            var conta = _contaClienteServico.Efetuar(1, 1, 1);

            int ret = conta.Where(c => c == "Conta Origem e Destino devem ser diferentes!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void ContaOrigemNaoEncontrada()
        {
            var conta = _contaClienteServico.Efetuar(10, 1, 1);

            int ret = conta.Where(c => c == "Conta não encontrada!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void ContaDestinoNaoEncontrada()
        {
            var conta = _contaClienteServico.Efetuar(1, 10, 1);

            int ret = conta.Where(c => c == "Conta não encontrada!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void OperacaoConcluida()
        {
            var conta = _contaClienteServico.Efetuar(1, 2, 10);

            int ret = conta.Where(c => c == "Operação concluída!").Count();

            Assert.Equal(1, ret);
        }

        [Fact]
        public void SaldoInsuficiente()
        {
            var conta = _contaClienteServico.Efetuar(1, 2, 100000000);

            int ret = conta.Where(c => c == "Conta 1 sem saldo!").Count();

            Assert.Equal(1, ret);
        }
    }
}