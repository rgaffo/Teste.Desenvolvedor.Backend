using Superdigital.Backend.ContaCorrente.Model;
using System;
using System.Collections.Generic;

namespace Superdigital.Backend.ContaCorrente.AppService
{
    public interface IOperacaoServico : IDisposable
    {
        ICollection<string> Efetuar(DadosConta dadosConta);
    }
}