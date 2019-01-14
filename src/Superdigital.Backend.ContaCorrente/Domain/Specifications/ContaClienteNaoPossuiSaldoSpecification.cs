using Superdigital.Backend.ContaCorrente.Domain.Entities;

namespace Superdigital.Backend.ContaCorrente.Domain.Specifications
{
    public class ContaClienteNaoPossuiSaldoSpecification : ISpecification<ContaCliente>
    {
        public string Mensagem { get; set; }

        public bool IsSatisfiedBy(ContaCliente entity, double Valor)
        {
            var retorno = false;

            if (entity != null)
            {
                if (entity.Saldo < Valor)
                {
                    Mensagem = string.Format("Conta {0} sem saldo!", entity.ContaClienteId);
                    return true;
                }
            }

            return retorno;
        }
    }
}
