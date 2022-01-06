# donus-code-challenge

Projeto desenvolvido de acordo com os requisitos especificados no repositório da [devdigitalrepublic](https://github.com/devdigitalrepublic/donus-code-challenge/blob/master/backend.md). 

O projeto representa uma API REST para gerenciamento de contas bancárias. As funcionalidades incluem depósitos, tranferências e saques, além de funções básicas de gerenciamento da conta e controle de acesso.

## Executando o Projeto

A maneira mais prática e ágil para executar o projeto é via docker compose.

### Executando com Docker Compose

#### Requisitos

- [Docker](https://www.docker.com/get-started) (v20.10.11+ recomendada)
- [Docker Compose](https://docs.docker.com/compose/install/) (normalmente incluso ao instalar o docker) 

Certifique que o Docker está em execução, e, no diretório raiz do projeto, execute no terminal:

    docker-compose up -d

Aguarde para que o Docker baixe as dependências e configure a aplicação, e pronto!

Para parar a execução, digite:

    docker-compose down

### Executando com dotnet CLI

#### Requisitos

- [.Net SDK v5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
- [SQL Server Express 2017](https://www.microsoft.com/pt-br/download/details.aspx?id=55994) ou [SSDT](https://docs.microsoft.com/pt-br/sql/ssdt/download-sql-server-data-tools-ssdt?view=sql-server-ver15)
- Execute o arquivo ```IBankDB/IBankDB.sql``` no banco de dados para criação das tabelas necessárias para o funcionamento do sistema. Alternativamente, publique o projeto ```IBankDB```, utilizando o Visual Studio.
- Insira a Connection String no local indicado no arquivo ```IBank/appsettings.json```

Para executar utilizando o dotnet diretamente, no diretório ```IBank```, abra o terminal e entre com o código abaixo:

    dotnet restore

E em seguida:

    dotnet run

--- 

## API Pública

Rotas com 🔒 podem ser acessadas informando um Bearer token, obtido na rota de login, informando no cabeçalho da requisição o parâmetro ```Authentication: Bearer {token}```.

### Account

    GET http://localhost:5000/api/v1/accounts/balance 🔒 # Your account balance
    GET http://localhost:5000/api/v1/accounts/my 🔒  # Your account details
    POST http://localhost:5000/api/v1/accounts # New account
    DELETE http://localhost:5000/api/v1/accounts 🔒 # Delete your account

Considere a seguinte estrutura para o body da requisição da criação de novas contas:

    {
	    "Name": "John Doe",
	    "Cpf": "xxx.xxx.xxx.xx",
	    "ShortPassword": "1234",
	    "Password": "123456"
    }

### Auth

    GET http://localhost:5000/api/v1/auth/me 🔒 # Personal info
    POST http://localhost:5000/api/v1/auth/login # Login

Considere a seguinte estrutura para o body da requisição de login:

    {
      "agency": {
        "number": "0001",
        "digit": "0"
      },
      "number": "12345",
      "digit": "0",
      "shortPassword": "1234"
    }


### Transaction

    GET http://localhost:5000/api/v1/transactions 🔒 # List of transactions
    POST http://localhost:5000/api/v1/transactions/deposit # Deposit to an account
    POST http://localhost:5000/api/v1/transactions/transfer 🔒 # Transfer to an account
    POST http://localhost:5000/api/v1/transactions/withdraw 🔒  # Withdraw from your account

Para listagem de transações, informe ```startDate=YYYY-MM-DD``` e ```endDate=YYYY-MM-DD```.

Para depósitos e transferências:

    {
      "addresse": {
        "name": "John Doe",
        "cpf": "xxx.xxx.xxx-xx",
        "account": {
          "agency": {
            "number": "0001",
            "digit": "0"
          },
          "number": "12345",
          "digit": "0"
        }
      },
      "amount": 1234.56
    }
    
E para saques:

    {
      "amount": 1234.56
    }

---

## Testes

A aplicação conta com um esquema de testes unitários que cobrem os controllers e services da aplicação. 

#### Requisitos

- [.Net SDK v5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)


Para executar os testes, entre no diretório ```IBank.UnitTests``` e insira o seguinte comando no terminal:
    
    dotnet test


