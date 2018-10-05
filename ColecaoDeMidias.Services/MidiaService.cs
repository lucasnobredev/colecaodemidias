using ColecaoDeMidias.Data;
using ColecaoDeMidias.Domain;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ColecaoDeMidias.Services
{
    public class MidiaService : IMidiaService
    {
        private readonly IESClientProvider esClientProvider;
        private int id = 1;

        public MidiaService(
            IESClientProvider esClientProvider)
        {
            this.esClientProvider = esClientProvider;
        }

        public IServiceResult CadastrarLivro(string descricao, string nomeDoAutor, string titulo, int quantidadeDePaginas)
        {
            if (string.IsNullOrEmpty(descricao) || string.IsNullOrEmpty(nomeDoAutor) || string.IsNullOrEmpty(titulo) || quantidadeDePaginas == 0)
                return ServiceResult.CriarFormularioInvalido(new List<string>() { "Preencha todos os campos" });

            var livro = new Livro(titulo, nomeDoAutor) { Descricao = descricao, QuantidadeDePaginas = quantidadeDePaginas };
                       
            ObterNovoMidiaId();

            Salvar(livro);

            return ServiceResult.CreateResultOk();
        }

        public IServiceResult CadastrarCd(string descricao, string nomeDoInterprete, string titulo, int quantidadeDeMusicas)
        {
            if (string.IsNullOrEmpty(descricao) || string.IsNullOrEmpty(nomeDoInterprete) || string.IsNullOrEmpty(titulo) || quantidadeDeMusicas == 0)
                return ServiceResult.CriarFormularioInvalido(new List<string>() { "Preencha todos os campos" });

            var cd = new Cd(titulo, nomeDoInterprete) { Descricao = descricao, QuantidadeDeMusicas = quantidadeDeMusicas };

            ObterNovoMidiaId();

            Salvar(cd);

            return ServiceResult.CreateResultOk();
        }

        public IServiceResult CadastrarDvd(string descricao, string titulo, string nomeDaGravadora, string idioma)
        {
            if (string.IsNullOrEmpty(descricao) || string.IsNullOrEmpty(nomeDaGravadora) || string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(idioma))
                return ServiceResult.CriarFormularioInvalido(new List<string>() { "Preencha todos os campos" });

            var dvd = new Dvd() { Descricao = descricao, Titulo = titulo, NomeDaGravadora = nomeDaGravadora, Idioma = idioma };

            ObterNovoMidiaId();

            Salvar(dvd);

            return ServiceResult.CreateResultOk();
        }

        public IServiceResult Devolver(TipoMidia tipoMidia, int midiaId)
        {
            if (tipoMidia == 0 || midiaId == 0)
                return ServiceResult.CriarFormularioInvalido(new List<string> { "Midia não informada" });

            var possuinte = new Possuinte() { Nome = string.Empty, FormaDeContato = string.Empty };

            var emprestimo = new Emprestimo(possuinte) { EstaEmprestado = false };

            switch (tipoMidia)
            {
                case TipoMidia.Livro:
                    esClientProvider.Client.Update<Livro, object>(midiaId, u => u.Doc(new { emprestimo = emprestimo}).RetryOnConflict(1));
                    break;
                case TipoMidia.Cd:
                    esClientProvider.Client.Update<Cd, object>(midiaId, u => u.Doc(new { emprestimo = emprestimo }).RetryOnConflict(1));
                    break;
                case TipoMidia.Dvd:
                    esClientProvider.Client.Update<Dvd, object>(midiaId, u => u.Doc(new { emprestimo = emprestimo }).RetryOnConflict(1));
                    break;
                default: throw new Exception();
            }

            return ServiceResult.CreateResultOk();
        }

        public IServiceResult Emprestar(TipoMidia tipoMidia, int midiaId, string possuinteNome, string possuinteFormaDeContato)
        {
            var possuinte = new Possuinte() { Nome = possuinteNome, FormaDeContato = possuinteFormaDeContato };

            var emprestimo = new Emprestimo(possuinte) { EstaEmprestado = true };          

            switch (tipoMidia)
            {
                case TipoMidia.Livro:
                    esClientProvider.Client.Update<Livro, object>(midiaId, u => u.Doc(new { emprestimo = emprestimo }).RetryOnConflict(1));
                    break;
                case TipoMidia.Cd:
                    esClientProvider.Client.Update<Cd, object>(midiaId, u => u.Doc(new { emprestimo = emprestimo }).RetryOnConflict(1));
                    break;
                case TipoMidia.Dvd:
                    esClientProvider.Client.Update<Dvd, object>(midiaId, u => u.Doc(new { emprestimo = emprestimo }).RetryOnConflict(1));
                    break;
                default: return ServiceResult.CriarFormularioInvalido(new List<string> { "Midia não informada" });
            }

            return ServiceResult.CreateResultOk();
        }
        
        public IList<Midia> ObterTodas()
        {
            var response = esClientProvider.Client.Search<IMidia>(x => x.Type(Types.Type(
                     typeof(Livro),
                     typeof(Cd),
                     typeof(Dvd))));

            if (!response.IsValid)
                throw new InvalidOperationException(response.DebugInformation);

            var docs = response.Hits.Select(hit =>
            {
                hit.Source.Id = Convert.ToInt32(hit.Id);
                return hit.Source;
            }
            ).OfType<Midia>().ToList();

            return docs.ToList();
        }

        public IList<Midia> ObterPeloFiltro(TipoMidia tipoMidia, StatusMidia statusMidia, string palavraChave)
        {
            if (tipoMidia == 0 && statusMidia == 0 && string.IsNullOrEmpty(palavraChave)) return ObterTodas();

            var response = esClientProvider.Client.Search<IMidia>(x => x.Type(Types.Type(
                     typeof(Livro),
                     typeof(Cd),
                     typeof(Dvd)))
                .Query(q =>
                (
                    q.Match(m => m.Field("_all").Query(palavraChave)) &&

                    (statusMidia != 0 ?
                        statusMidia == StatusMidia.Disponivel ?
                          q.Term(t => t.Field(f => f.Emprestimo.EstaEmprestado)
                          .Value(false)) :
                        q.Term(t => t.Field(f => f.Emprestimo.EstaEmprestado)
                          .Value(true))
                    : null) &&

                    (tipoMidia != 0 ?
                    +q.Term("_type", tipoMidia.ToString().ToLower())
                    : null)

                ))
            );

            var docs = response.Hits.Select(hit =>
            {
                hit.Source.Id = Convert.ToInt32(hit.Id);
                return hit.Source;
            }
            ).OfType<Midia>().ToList();

            return docs;
        }

        private void ObterNovoMidiaId()
        {
            var searchResponse = ObterTodas();

            if (searchResponse.Count() > 0)
                id = searchResponse.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
        }

        private void Salvar<T>(T obj) where T : class
        {
            var indexResponse = esClientProvider.Client.Index(obj, x => x.Id(id.ToString()));

            if (!indexResponse.IsValid)
                throw new InvalidOperationException(indexResponse.DebugInformation);
        }

    }
}