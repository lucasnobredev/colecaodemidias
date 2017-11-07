using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ColecaoDeMidias.Models;
using ColecaoDeMidias.Services;

namespace ColecaoDeMidias.Controllers
{
    [Route("api/[controller]")]
    public class LivroController : Controller
    {
        private readonly IMidiaService midiaService;

        public LivroController(
            IMidiaService midiaService)
        {
            this.midiaService = midiaService;
        }
               
        [HttpPost]
        public IActionResult Create(LivroViewModel form)
        {
            var result = midiaService.CadastrarLivro(form.Descricao, form.NomeDoAutor, form.Titulo, form.QuantidadeDePaginas);
            if (!result.EhValido)
                return Json(new { erros = result.Erros });

            return Ok();
        }
    }
}