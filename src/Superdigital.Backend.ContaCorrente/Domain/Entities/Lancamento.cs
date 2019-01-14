namespace Superdigital.Backend.ContaCorrente.Domain.Entities
{
    public class Lancamento
    {
        public int LancamentoId { get; set; }
        public int ContaClienteId { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public double Valor { get; set; }

        public ContaCliente ContaCliente { get; set; }
    }
}