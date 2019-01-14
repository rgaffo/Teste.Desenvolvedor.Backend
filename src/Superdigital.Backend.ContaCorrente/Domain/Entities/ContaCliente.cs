using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.Domain.Entities
{
    public class ContaCliente
    {
        public int ContaClienteId { get; set; }
        public string NomeCliente { get; set; }
        public double Saldo { get; set; }

        public IEnumerable<Lancamento> Lancamentos { get; set; }
    }
}