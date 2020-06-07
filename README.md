# Easyinvest-Case

## Projeto ClearSale TestDgBar

### O que é?

Este projeto é a solução para o problema enviado pela Empresa ClearSale.
Este arquivo Readme tem o objetivo de esclarecer as decisões e *frameworks* (pacotes) utilizados no projeto para a solução do problema.

## Definição do Projeto

A seguir são descritas as decisões utilizadas no projeto:

### Plataforma
Foi utilizado para desenvolvimento a versão Core 3 por ser a versão mais atual da plataforma .Net.

### Arquitetura de software
Para este projeto adotei a abordagem de modelagem de software **DDD (*Domain Driven Design*)** focando em resolver o problema do negócio guiado pelo domínio, estruturando-o em camadas de domínio, aplicação (serviços) e infraestrutura. O  projeto físico foi criado utilizando-se a concepção de arquitetura cebola (*onion architecture*) devido às camadas em que elas estão associadas.

### Estratégia de autenticação
Para autenticação entre as camadas Web Api e Web Application, foi utilizado JWT (*JSON web token*)  com *Bearer authentication* como esquema para autenticação HTTP. Por se tratar de uma implementação OAuth2 bem popular em desenvolvimento web. Além de ser um padrão aberto, ele permite a transmissão de dados em formato JSON de maneira segura e compacta. JWTs não usam sessões e são independentes, sendo ideais para arquiteturas de micro-services.
O serviço de autenticaçao foi desenvolvido em um microserviço separado pois assim pode-se demosnrtar como microserviços distintos podem comunicar-se entre sí.
Ponto de evolução: pode-se evoluir a autenticação para uso de serviços de autenticação OAuth2 OpenId Connect como por exemplo Facebook, Google e contas da Microsoft.


### Divisão da solução
A seguir é descrito como a solução foi divida em projetos:

 - **ClearSaleProva.TestDgBar.Dominio**: Este projeto define a camada de dominio da arquitetura. Contém as entidades, *value objects* e interfaces utilizados no sistema, além dos serviços relacionados aos comportamentos das entidades.
 - **ClearSaleProva.TestDgBar.Infra**: Este projeto realiza a persistência das entidades se comunicando diretamente com o repositório.
 - **ClearSaleProva.TestDgBar.Aplicacao**: Este projeto define a camada de aplicação, contendo todos os serviços e lógicas de négocio as quais o problema se propõe a resolver. Esta camada orquestra as ações disparadas pela camada de apresentação se comunicando com as camadas de infraestrutura e domínio, além de fornecer o resultado a ser consumida pela camada de apresentação.
 - **ClearSaleProva.TestDgBar.Api**: Projeto da camada de apresentação responsável por expor o serviço e disparar as ações na camada de aplicação para gerenciar uma comanda. 
 - **ClearSaleProva.TestDgBar.Identity**: Este projeto é responsável pela autenticação. Pertence à camada de apresentação expondo o endpoint para autenticação. Por se tratar de um serviço simples, possui seus próprios serviços.
 - **ClearSaleProva.TestDgBar.Web**: Projeto Web Application desenvolvido em ASP.Net Core MVC para agir como cliente consumindo as APIs dos microserviços de autenticação e comanda.

## Decisões de implementação
Durante a implementação foram adotadas as seguintes decisões para implementação do projeto:

### CQRS
Foi utilizado CQRS como um padrão arquitetural para separar os processos de leitura e escrita de dados possibilitando dividir as operações em *Queries* e *Commands*. A ideia por trás disso é poder realizar otimizaçoes para ambos os casos, utilizando-se diferentes abordagens para se lidar com o repositório. Por exemplo para leitura (*queries*), pode-se utilizar um micro-ORM por motivos de performance, enquanto que para gravação, pode-se utilizar um *entity framework* ou serviço de mensagens. Em tese, gravação de dados necessita de tratamento e por isso pode-se tornar mais lenta. A separação permite que a tarefa seja tratada em segundo plano, liberando a aplicação para outras tarefas tornando-a mais responsiva para o usuário.

Neste projeto utilizei o EF Core para ambos as operações, por se tratar de um repositório bem simples. Como ponto de melhoria neste projeto, as operações de leitura poderiam ser efetuadas utilizando-se um micro-ORM tal como *Dapper*. As operações de gravação poderiam ser colocadas em uma fila de mensagens, como por exemplo RabbitMQ, permitido que essas operações possam ser feitas em segundo plano com a vantagem de estarem persistidas na fila até sua efetiva gravação.

### Builder
De acordo com o consumo do cliente, uma forma de promoção deve ser aplicada. Ao calcular o total da comanda tem-se com um problema que é a diferença no cálculo das promoções. Para solucionar este problema, foi utilizado o design pattern ***Builder*** pois este padrão permite separar a construção de um objeto complexo da sua representação, de forma que o diferentes processos de construção possam criar uma única representação. 
### MediatR 
Utilizei neste projeto o framework [MediatR](https://github.com/jbogard/MediatR) que é a implementação do design patter ***Mediator***. Utilizei esta implementação para obter baixo acoplamento entre a camada de apresentação e a camada de aplicação, permitindo que ambas se comuniquem sem conhecer suas estruturas. Este padrão age como ponto central que encapsula os objetos que irão se comunicar entre sí.
### Polly
No projeto cliente, foi utilizado o framework ***Polly*** para implementar implementa o padrão de tentativas (*retry pattern*) para acesso aos endpoints dos microserviços permitindo melhor resiliência no tratamento de falhas de comunicação.
Como ponto de melhoria, deve-se implementar a política de *circuit-breaker* para quando o sistema remoto está irresponsível.
### xUnit
Utilizei no projeto de testes unitários o framework ***xUnit*** por ser uma evolução do ***nUnit*** e porque este ultimo tem evoluido mais lentamente que a propria plataforma .Net.
### Resiliencia
Foi utilizado como estratégia de resiliencia de banco de dados,  *Connection Resiliency* para repetir comandos enviados ao bancode dados que falharam. 
Como ponto de melhoria, pode-se implementar uma estrategia de execução customizada para logar erros ocorridos e, por exemplo no caso de impossibilidade de gravar dados, coloca-los em uma fila para posterior tratamento.
Para monitoria do estado da infraestrutura, implementei o middleware Health Checks para reportar o estado dos componentes da infraestrutura. Neste caso considerei a base de dados como infraestrutura a ser monitorada.
O estado da infraestrutura pode ser verificado acessando o endpoint gerado pelo middleware em [https://localhost:44363/hc](https://localhost:44363/hc)

## Utilização
Antes de iniciar a aplicação pela primeira vez, deve-se seguir os seguintes passos para que a base de dados seja criada e inicializada:

 1. Abrir o projeto no Visual Studio 2019
 2. Selecionar o projeto **ClearSaleProva.TestDgBar.Api** como projeto *Startup Project*
 3. No Console, selecionar o projeto **ClearSaleProva.TestDgBar.Infra** no combobox Default Project
 4. No Console, executar o comando
      >     update-database
5. Aguardar a criação do database
6. Abrir a caixa de propriedades da solution
7. Selecionar a opção Multiple startup projects

Para efetuar o Login:
Login: **admin**
Senha: **admin**