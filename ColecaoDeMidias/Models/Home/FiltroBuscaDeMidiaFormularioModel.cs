using ColecaoDeMidias.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.Models.Home
{
    public class FiltroBuscaDeMidiaFormularioModel
    {
        public TipoMidia TipoMidia { get; set; }
        public StatusMidia StatusMidia { get; set; }
        public string PalavraChave { get; set; }
    }
}
