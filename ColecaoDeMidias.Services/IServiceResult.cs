using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Services
{
    public interface IServiceResult
    {
        bool EhValido { get; }
        IList<string> Erros { get; }
    }
}
