using System;
using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.Domain.Services.Operacao
{
    public interface IOperacao : IDisposable
    {
        ICollection<string> Efetuar(int Id, double valor);
    }
}