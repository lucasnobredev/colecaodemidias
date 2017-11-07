using System;
using System.Collections.Generic;

namespace ColecaoDeMidias.Domain
{
    public interface IMidia
    {
        int Id { get; set; }
        string Titulo { get; set; }
        string Descricao { get; set; }
        int AnoDeLancamento { get; set; }
        Emprestimo Emprestimo { get; set; }
    }
}
