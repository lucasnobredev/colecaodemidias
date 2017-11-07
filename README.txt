=== SISTEMA ===
Fiz um sistema bem simples. Procurei ser objetivo e n�o estender muito a minha abstra��o.

Eu criei 4 controllers: midia, livro, cd e dvd.
A principio eu poderia ter colocado tudo em midia, mas achei que ficaria bagun�ado e eu queria seguir o padr�o de WebApi (api/controller/).
Ent�o para o cadastro, algo mais espec�fico para cada tipo, ficou mais amig�vel a url (api/livro, api/cd, api/dvd).
Por�m, para todas as outras a��es que envolviam Midia em geral, como emprestar, devolver e obter todos, eu pus no controller Midia.

Criei uma camada de aplica��o para Midia, j� que � o foco do sistema e tirei toda a responsabilidade do controller 
de saber como obt�-la, cadastr�-la, emprest�-la e devolv�-la.

Al�m disso, os dados que s�o obtidos do dom�nio s�o devidamente mapeados para viewModels. Esta pr�tica permite uma boa
futura manuten��o e separa as responsabilidades de cada model. Ou seja, as regras de dom�nio nada tem a ver com as de ViewModel.
Um bom exemplo* s�o os atributos decorados no dom�nio "[Text]", relacionados as propriedades dos documentos em ElasticSearch.

*OBS: Em um contexto onde eu tenha um banco relacional como prim�rio e o ElasticSearch como secund�rio, provavelmente a decora��o
do atributo n�o seria em dom�nio. No exemplo que dei a voc�s na entrevista sobre o uso do Elastic considerava justamente este exemplo.
Supondo que eu seguisse minha ideia e estivesse usando CQRS, as ViewModels seriam boas candidatas para receberem os documentos do Elastic.

E por mais que eu esteja trabalhando com dominio e acesso a dados do mesmo, n�o vi necessidade de incluir o Repository pattern.
Motivo: YAGNI. 
O mais importante eu j� estava fazendo: n�o permitir que o controller conhecesse o "como fazer X".

=== VALIDA��O E TESTES ===
Apesar dos campos em html estarem com required, adicionei algumas valida��es para proteger o sistema de eventuais manipula��es do html,
assim como ter um sistema test�vel.

Eu usei o NUnit. Fiz 4 m�todos de teste, em que acredito que me ajudaram na cria��o e numa futura manuten��o do sistema.
Procurei seguir alguns padr�es para teste, como o in�cio do m�todo com Should (Deveria) e o System Under Test - SUT.
Al�m disso, usei o Moq para criar a depend�ncia fake do meu MidiaService.

=== ELASTICSEARCH E DOCKER ===
Como voc�s comentaram na entrevista sobre o Docker, decidi ler sobre e aplic�-lo em meu desafio.
Ent�o, ao inv�s de simplesmente baixar e instalar o ElasticSearch e Kibana, eu instalei o Docker para Windows e a partir dele, o ES e Kibana.

O que eu fiz:
- Eu achei a imagem "nshou/elasticsearch-kibana" no Docker Hub - https://hub.docker.com/r/nshou/elasticsearch-kibana/;
- Iniciei meu container com esta imagem com o seguinte comando:
	docker run -d -p 9200:9200 -p 5601:5601 nshou/elasticsearch-kibana

A partir disso, consegui desenvolver toda a aplica��o me conectando ao ES pelo localhost:9200 e o Kibana pelo localhost:5601.
Por�m, no curso que fiz no PluralSight sobre Docker (Docker Deep Dive), ele citou o dockerfile. 
Ent�o criei o meu pr�prio, com um pouquinho de ajuda da internet, para a configura��o do ambiente. Ele est� raiz.

Para us�-lo, eu fiz:
	docker build -t {nomeParaAImagem}
	docker run -it -p 9200:9200 --name {nomeDoContainer} -d {IMAGE} /bin/bash

Obs: eu achei o uso do dockerfile lento demais. Provavelmente o script poderia ser feito de outra forma mais pr�tica e leve.
Ent�o eu recomendo na hora de testar a minha aplica��o usar a primeira forma, com a imagem do Docker Hub. Ela foi bem mais r�pida.

=== CONSIDERA��ES FINAIS ===
Pessoal, gostei bastante do desafio. Acredito que evolui muito nos �ltimos dias. Vi bastante coisa nova de ElasticSearch,
assim como de Docker, do qual nunca havia feito nada.
Sobre o .NET Core e WebApi, achei muito tranquilo e bem mais f�cil. N�o tenho nem o que falar sobre Inje��o de dependencia nativa... 
Simplesmente sensacional.

� isso, pessoal. Obrigado pela oportunidade e espero que gostem do meu trabalho.

--
Autor:
Lucas Nobre