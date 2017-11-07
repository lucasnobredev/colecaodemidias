using System;
using System.Collections.Generic;
using System.Text;

namespace ColecaoDeMidias.Services
{
    public class ServiceResult : IServiceResult 
    {
        public bool EhValido => Erros.Count == 0;
        public IList<string> Erros { get; private set; }

        public ServiceResult()
        {
            Erros = new List<string>();
        }

        public static ServiceResult CreateResultOk()
        {
            return new ServiceResult() { };
        }
        
        public static ServiceResult CriarFormularioInvalido(IList<string> erros)
        {
            return new ServiceResult() { Erros = erros };
        }
    }
}
