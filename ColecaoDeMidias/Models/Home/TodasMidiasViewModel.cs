using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.Models.Home
{
    public class TodasMidiasViewModel
    {
        public IList<LivroViewModel> Livros { get; set; }
        public IList<CdViewModel> Cds { get; set; }
        public IList<DvdViewModel> Dvds{ get; set; }
    }
}
