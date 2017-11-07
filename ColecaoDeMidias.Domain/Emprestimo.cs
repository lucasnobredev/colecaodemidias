using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Domain
{
    public class Emprestimo
    {
        public Possuinte Possuinte { get; private set; }
        public DateTime DataDeUltimaAcao => DateTime.Now;
        public bool EstaEmprestado { get; set; }
            
        public Emprestimo(Possuinte possuinte)
        {
            this.Possuinte = possuinte;
        }
    }
}
