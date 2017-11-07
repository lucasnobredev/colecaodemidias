using ColecaoDeMidias.Domain;
using System.Collections.Generic;

namespace ColecaoDeMidias.Services
{
    public interface IMidiaService
    {
        IServiceResult CadastrarLivro(string descricao, string nomeDoAutor, string titulo, int quantidadeDePaginas);
        IServiceResult CadastrarCd(string descricao, string nomeDoInterprete, string titulo, int quantidadeDeMusicas);
        IServiceResult CadastrarDvd(string descricao, string titulo, string nomeDaGravadora, string idioma);
        IServiceResult Emprestar(TipoMidia tipoMidia, int midiaId, string possuinteNome, string possuinteFormaDeContato);
        IServiceResult Devolver(TipoMidia tipoMidia, int midiaId);
        IList<Midia> ObterTodas();
        IList<Midia> ObterPeloFiltro(TipoMidia tipoMidia, StatusMidia statusMidia, string palavraChave);
    }
}