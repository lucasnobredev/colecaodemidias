using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Domain
{
    public class Livro : Midia
    {
        [Text]
        public string NomeDoAutor { get; private set; }
        [Text]
        public int QuantidadeDePaginas { get; set; }
        

        public Livro(string titulo, string nomeDoAutor)
        {
            this.Titulo = titulo;
            this.NomeDoAutor = nomeDoAutor;
        }
    }
}
