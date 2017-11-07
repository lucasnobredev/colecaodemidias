using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Domain
{
    public class Dvd : Midia
    {
        [Text]
        public string NomeDaGravadora { get; set; }
        [Text]
        public string Idioma { get; set; }
    }
}
