using Superdigital.Backend.ContaCorrente.Data.Context;
using Superdigital.Backend.ContaCorrente.Domain.Entities;
using Superdigital.Backend.ContaCorrente.Domain.Interfaces;

namespace Superdigital.Backend.ContaCorrente.Data.Repositories
{
    public class ContaClienteRepository : Repository<ContaCliente>, IContaClienteRepository
    {
        public ContaClienteRepository(ContaCorrenteContext contaCorrenteContext) : base(contaCorrenteContext)
        {

        }
    }
}