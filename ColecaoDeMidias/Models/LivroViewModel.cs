using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.Models
{
    public class LivroViewModel : MidiaViewModel
    {
        public string NomeDoAutor { get; set; }
        public int QuantidadeDePaginas { get; set; }
    }
}
