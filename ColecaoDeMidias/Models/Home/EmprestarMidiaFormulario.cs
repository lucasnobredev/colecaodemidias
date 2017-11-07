using ColecaoDeMidias.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.Models.Home
{
    public class EmprestarMidiaFormulario
    {
        public TipoMidia TipoMidia { get; set; }
        public int MidiaId { get; set; }
        public string PossuinteNome { get; set; }
        public string PossuinteFormaDeContato { get; set; }
    }
}
