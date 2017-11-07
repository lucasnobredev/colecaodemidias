using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Domain
{
    public class Midia : IMidia
    {
        [Number(Ignore =true)]
        public int Id { get; set; }
        [Text]
        public string Descricao { get; set; }
        [Text]
        public string Titulo { get; set; }
        [Text]
        public int AnoDeLancamento { get; set; }

        public Emprestimo Emprestimo { get; set; }

        public Midia()
        {
            this.Emprestimo = new Emprestimo(new Possuinte());
        }
    }
}
