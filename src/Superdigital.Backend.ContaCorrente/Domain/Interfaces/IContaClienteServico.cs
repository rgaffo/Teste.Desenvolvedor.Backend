using System;
using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.Domain.Interfaces
{
    public interface IContaClienteServico : IDisposable
    {
        ICollection<string> Efetuar(int ContaOrigemId, int ContaDestinoId, double Valor);
    }
}