using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.Models
{
    public class CdViewModel : MidiaViewModel
    {
        public string NomeDoInterprete { get; set; }
        public int QuantidadeDeMusicas { get; set; }
    }
}
