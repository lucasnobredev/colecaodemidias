using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ColecaoDeMidias.Services;
using ColecaoDeMidias.Models.Home;
using ColecaoDeMidias.Domain;
using ColecaoDeMidias.Models;
using AutoMapper;

namespace ColecaoDeMidias.Controllers
{
    public class MidiaController : Controller
    {
        private readonly IMidiaService midiaService;

        public MidiaController(
            IMidiaService midiaService)
        {
            this.midiaService = midiaService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Cadastrar");
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [Route("api/[controller]/search")]
        [HttpPost]
        public IActionResult ObterTodasMidiasPorFiltro(FiltroBuscaDeMidiaFormularioModel form)
        {
            var midias = midiaService.ObterPeloFiltro(form.TipoMidia, form.StatusMidia, form.PalavraChave);

            var viewModel = CriarTodasMidiasViewModel(midias);

            return PartialView("_MidiasBuscadas", viewModel);
        }

        [Route("api/[controller]/emprestar")]
        [HttpPost]
        public IActionResult Emprestar(EmprestarMidiaFormulario form)
        {
            var result = midiaService.Emprestar(form.TipoMidia, form.MidiaId, form.PossuinteNome, form.PossuinteFormaDeContato);

            if (!result.EhValido)
                return Json(new { erros = result.Erros });

            return Ok();
        }

        [Route("api/[controller]/devolver")]
        [HttpPost]
        public IActionResult Devolver(TipoMidia tipoMidia, int midiaId)
        {
            var result = midiaService.Devolver(tipoMidia, midiaId);

            if (!result.EhValido)
                return Json(new { erros = result.Erros });

            return Ok();
        }


        private TodasMidiasViewModel CriarTodasMidiasViewModel(IList<Midia> midias)
        {
            return new TodasMidiasViewModel()
            {
                Livros = Mapper.Map<IList<LivroViewModel>>(midias.OfType<Livro>().ToList()),
                Cds = Mapper.Map<IList<CdViewModel>>(midias.OfType<Cd>().ToList()),
                Dvds = Mapper.Map<IList<DvdViewModel>>(midias.OfType<Dvd>().ToList())
            };
        }
    }
}