=== SISTEMA ===
Fiz um sistema bem simples. Procurei ser objetivo e não estender muito a minha abstração.

Eu criei 4 controllers: midia, livro, cd e dvd.
A principio eu poderia ter colocado tudo em midia, mas achei que ficaria bagunçado e eu queria seguir o padrão de WebApi (api/controller/).
Então para o cadastro, algo mais específico para cada tipo, ficou mais amigável a url (api/livro, api/cd, api/dvd).
Porém, para todas as outras ações que envolviam Midia em geral, como emprestar, devolver e obter todos, eu pus no controller Midia.

Criei uma camada de aplicação para Midia, já que é o foco do sistema e tirei toda a responsabilidade do controller 
de saber como obtê-la, cadastrá-la, emprestá-la e devolvê-la.

Além disso, os dados que são obtidos do domínio são devidamente mapeados para viewModels. Esta prática permite uma boa
futura manutenção e separa as responsabilidades de cada model. Ou seja, as regras de domínio nada tem a ver com as de ViewModel.
Um bom exemplo* são os atributos decorados no domínio "[Text]", relacionados as propriedades dos documentos em ElasticSearch.

*OBS: Em um contexto onde eu tenha um banco relacional como primário e o ElasticSearch como secundário, provavelmente a decoração
do atributo não seria em domínio. No exemplo que dei a vocês na entrevista sobre o uso do Elastic considerava justamente este exemplo.
Supondo que eu seguisse minha ideia e estivesse usando CQRS, as ViewModels seriam boas candidatas para receberem os documentos do Elastic.

E por mais que eu esteja trabalhando com dominio e acesso a dados do mesmo, não vi necessidade de incluir o Repository pattern.
Motivo: YAGNI. 
O mais importante eu já estava fazendo: não permitir que o controller conhecesse o "como fazer X".

=== VALIDAÇÃO E TESTES ===
Apesar dos campos em html estarem com required, adicionei algumas validações para proteger o sistema de eventuais manipulações do html,
assim como ter um sistema testável.

Eu usei o NUnit. Fiz 4 métodos de teste, em que acredito que me ajudaram na criação e numa futura manutenção do sistema.
Procurei seguir alguns padrões para teste, como o início do método com Should (Deveria) e o System Under Test - SUT.
Além disso, usei o Moq para criar a dependência fake do meu MidiaService.

=== ELASTICSEARCH E DOCKER ===
Como vocês comentaram na entrevista sobre o Docker, decidi ler sobre e aplicá-lo em meu desafio.
Então, ao invés de simplesmente baixar e instalar o ElasticSearch e Kibana, eu instalei o Docker para Windows e a partir dele, o ES e Kibana.

O que eu fiz:
- Eu achei a imagem "nshou/elasticsearch-kibana" no Docker Hub - https://hub.docker.com/r/nshou/elasticsearch-kibana/;
- Iniciei meu container com esta imagem com o seguinte comando:
	docker run -d -p 9200:9200 -p 5601:5601 nshou/elasticsearch-kibana

A partir disso, consegui desenvolver toda a aplicação me conectando ao ES pelo localhost:9200 e o Kibana pelo localhost:5601.
Porém, no curso que fiz no PluralSight sobre Docker (Docker Deep Dive), ele citou o dockerfile. 
Então criei o meu próprio, com um pouquinho de ajuda da internet, para a configuração do ambiente. Ele está raiz.

Para usá-lo, eu fiz:
	docker build -t {nomeParaAImagem}
	docker run -it -p 9200:9200 --name {nomeDoContainer} -d {IMAGE} /bin/bash

Obs: eu achei o uso do dockerfile lento demais. Provavelmente o script poderia ser feito de outra forma mais prática e leve.
Então eu recomendo na hora de testar a minha aplicação usar a primeira forma, com a imagem do Docker Hub. Ela foi bem mais rápida.

=== CONSIDERAÇÕES FINAIS ===
Pessoal, gostei bastante do desafio. Acredito que evolui muito nos últimos dias. Vi bastante coisa nova de ElasticSearch,
assim como de Docker, do qual nunca havia feito nada.
Sobre o .NET Core e WebApi, achei muito tranquilo e bem mais fácil. Não tenho nem o que falar sobre Injeção de dependencia nativa... 
Simplesmente sensacional.

É isso, pessoal. Obrigado pela oportunidade e espero que gostem do meu trabalho.

--
Autor:
Lucas Nobre