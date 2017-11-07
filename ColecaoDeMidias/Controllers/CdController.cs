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
    public class CdController : Controller
    {
        private readonly IMidiaService midiaService;

        public CdController(
            IMidiaService midiaService)
        {
            this.midiaService = midiaService;
        }

        [HttpPost]
        public IActionResult Create(CdViewModel form)
        {
            midiaService.CadastrarCd(form.Descricao, form.NomeDoInterprete, form.Titulo, form.QuantidadeDeMusicas);

            return Ok();
        }
    }
}