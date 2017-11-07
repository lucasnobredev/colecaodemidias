using AutoMapper;
using ColecaoDeMidias.Domain;
using ColecaoDeMidias.Models;
using ColecaoDeMidias.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColecaoDeMidias.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Midia, TodasMidiasViewModel>();
            CreateMap<Livro, LivroViewModel>();
            CreateMap<Cd, CdViewModel>();
            CreateMap<Dvd, DvdViewModel>();
            CreateMap<Emprestimo, EmprestimoViewModel>();
                
        }
    }
}
