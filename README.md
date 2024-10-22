# Picpay Simplificado 

>Esse foi meu primeiro projeto em c# então provavelmente vai existir várias coisas fora do padrão 😆

API baseada no desafio picpay 
[Desafio PicPay](https://github.com/PicPay/picpay-desafio-backend), no qual seria um picpay simplificado, simulando transferências

A API por padrão vai rodar na porta **5000**


## Regras de negócio
A seguir estão algumas regras de negócio que são importantes para o funcionamento do PicPay Simplificado:

Para ambos tipos de usuário, precisamos do Nome Completo, CPF, e-mail e Senha. CPF/CNPJ e e-mails devem ser únicos no sistema. Sendo assim, seu sistema deve permitir apenas um cadastro com o mesmo CPF ou endereço de e-mail;

Usuários podem enviar dinheiro (efetuar transferência) para lojistas e entre usuários;

Lojistas só recebem transferências, não enviam dinheiro para ninguém;

Validar se o usuário tem saldo antes da transferência;

Antes de finalizar a transferência, deve-se consultar um serviço autorizador externo, use este mock https://util.devi.tools/api/v2/authorize para simular o serviço utilizando o verbo GET;

A operação de transferência deve ser uma transação (ou seja, revertida em qualquer caso de inconsistência) e o dinheiro deve voltar para a carteira do usuário que envia;

No recebimento de pagamento, o usuário ou lojista precisa receber notificação (envio de email, sms) enviada por um serviço de terceiro e eventualmente este serviço pode estar indisponível/instável. Use este mock https://util.devi.tools/api/v1/notify)) para simular o envio da notificação utilizando o verbo POST;

Este serviço deve ser RESTFul.

## Endpoints
Endpoint feito para criar um usuário

### Criar um usuário
```http request
POST /user
Content-Type: application/json

{
  "name": "string",
  "password": "string",
  "email": "user@example.com",
  "document": "string",
  "balance": 0,
  "walletType": "USER"
}
```

### Transferência 


```http request
POST /transfer
Content-Type: application/json

{
  "payer": 0,
  "payee": 0,
  "value": 100
}
```




### Documentação

Documentação feita com a ferramenta Swagger na rota **5000/swagger**


### Executar o projeto localmente

Pré-requisitos: 
- .NET 8.0
- Ferramentas do Entity Framework (EF) Core
- Docker

```bash
dotnet tool install --global dotnet-ef
```

Mude a **DefaultConnection** de acordo com o seu docker-compoose.yml 

**app.settings.json**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection":"Host=localhost;Username=user;Password=senha;Database=databasecsharp"
  },
  "AllowedHosts": "*"    
  
}
```

**docker-compoose.yml**
```yml
services:
  postgres:
    image: bitnami/postgresql:latest
    ports:
      - '5432:5432'
    environment:
      - POSTGRES_USER=user
      - POSTGRES_PASSWORD=senha
      - POSTGRES_DB=databasecsharp
    volumes:
      - postgres_data:/bitnami/postgresql
volumes:
  postgres_data:
```

#### Rodar as migrations

- Adicionar a migration
```bash
dotnet ef migrations add NomeDaMigration # Nome desejado da migration
```

- Sincronizar com o banco de dados

```bash
dotnet ef database update
```

```bash
# clonar o repositório
git clone https://github.com/bxzzxnx/DesafioPicPay
cd DesafioPicPay
dotnet restore

# iniciar o container do docker
docker-compose up -d

# executar o projeto
dotnet run
```
