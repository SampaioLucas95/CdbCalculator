# CdbCalculator

Projeto para cálculo de investimento em CDB

## Stack: .NET 8 e padrão com mediator para api rest. Foi utilizado Angular 18.1.0 para frontend.

### Como Executar

1. Clone o repositório:

git clone https://github.com/SampaioLucas95/CdbCalculator.git

2. A api pode ser executada com docker também, mas vai da escolha.

2.1 Navegue até o diretório do projeto e abra a solution CdbCalculator.sln e execute o projeto para rodar com o protocolo http.

2.2 Se quiser rodar com docker, só executar com o visual stúdio selecionando a opção Dockerfile, e ajuste a apiUrl no arquivo environment do projeto frontend.

3. Acesse a API em:
http://localhost:5001

4. Se for consumir a API sem o frontend, é necessário especificar a versão da API no campo version do Swagger:
http://localhost/swagger

5. Para rodar o frontend, é só acessar o diretorio src\CdbCalculator.Spa e executar:
npm i
npm start

6. Acesse o frontend em:
http://localhost:4200

Observação: estamos trabalhando para subir este projeto na nuvem, com CICD, docker compose com a criação de elastic para registro de logs.