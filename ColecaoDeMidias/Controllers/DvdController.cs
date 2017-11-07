using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ColecaoDeMidias.Services;
using ColecaoDeMidias.Models;

namespace ColecaoDeMidias.Controllers
{
    [Route("api/[controller]")]
    public class DvdController : Controller
    {
        private readonly IMidiaService midiaService;

        public DvdController(
            IMidiaService midiaService)
        {
            this.midiaService = midiaService;
        }

        [HttpPost]
        public IActionResult Create(DvdViewModel form)
        {
            var result = midiaService.CadastrarDvd(form.Descricao, form.Titulo, form.NomeDaGravadora, form.Idioma);

            if (!result.EhValido)
                return Json(new { erros = result.Erros });

            return Ok();
        }
    }
}