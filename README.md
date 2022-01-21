# Event-Driven: Outbox Pattern
Trabalhar em sistemas distribuídos é um grande desafio, dado a grande quantidade de conceitos e padrões computacionais que um engenheiro de sistemas precisa conhecer e saber aplicar.

Nesse tipo de sistemas, fica evidente que o estilo arquitetural Event-Driven começa a ser um padrão adotado para resolver problemas na comunicação entre os serviços, trazendo uma maior responsividade - _Neste caso, otimizar o tempo de resposta das aplicações_, às aplicações.

Implementar uma arquitetura distribída com base em Event-Driven traz alguns desafios, e um deles é a Garantia da Entrega de mensagens ou [Guaranteed Delivery](https://www.enterpriseintegrationpatterns.com/patterns/messaging/GuaranteedMessaging.html), que é um padrão de design com o qual, uma arquitetura passa a dar a capacidade de resposta de que, mesmo usando Microsserviços, a plataforma como um todo, passe a dar garantias de que as mensagens transitarão de um serviço para o outro, através de mensageria e processamentos em background.

Esse repositório traz a implementação do padrão arquitetural Outbox Pattern em um projeto usando as seguintes tecnologias:

- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (como Minimal API)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Apache Kafka](https://kafka.apache.org/)
- [Docker](https://www.docker.com/)

Para rodar o projeto, faça o clone do projeto e rode a linha de comando abaixo para subir os containers:

```shell
> docker-compose -f docker-compose.yaml up -d
```

⚠️ PS.1: O conteúdo do docker-compose contempla APENAS o **SQL** e o **Apache Kafka**.