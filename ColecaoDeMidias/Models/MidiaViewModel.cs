using ColecaoDeMidias.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.Models
{
    public class MidiaViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public EmprestimoViewModel Emprestimo { get; set; }        
    }
}
