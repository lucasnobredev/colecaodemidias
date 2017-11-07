using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Domain
{
    public class Cd : Midia
    {
        [Text]
        public string NomeDoInterprete { get; set; }
        [Text]
        public int QuantidadeDeMusicas { get; set; }
        
        public Cd(string titulo, string nomeDoInterprete)
        {
            this.Titulo = titulo;
            this.NomeDoInterprete = nomeDoInterprete;
        }
    }
}
