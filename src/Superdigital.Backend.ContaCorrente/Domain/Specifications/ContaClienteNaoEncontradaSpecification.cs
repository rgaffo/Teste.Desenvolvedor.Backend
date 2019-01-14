using Superdigital.Backend.ContaCorrente.Domain.Entities;
using System;

namespace Superdigital.Backend.ContaCorrente.Domain.Specifications
{
    public class ContaClienteNaoEncontradaSpecification : ISpecification<ContaCliente>
    {
        public string Mensagem { get; set; }

        public bool IsSatisfiedBy(ContaCliente entity, double Valor)
        {
            if (entity == null)
            {
                Mensagem = "Conta não encontrada!";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}