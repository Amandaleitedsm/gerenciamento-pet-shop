# 🐾 Gerenciamento PetShop API

Uma API robusta em **.NET** desenvolvida para gerenciar as operações diárias de um PetShop. O sistema permite o controle completo de clientes e seus respectivos animais de estimação (pets), além de incluir autenticação segura, upload de arquivos (como fotos dos pets) e uma arquitetura limpa em camadas.

---

## 🛠️ Tecnologias Utilizadas

Este projeto foi construído utilizando as seguintes tecnologias e práticas:

* **C# e .NET 8/10** (ASP.NET Core Web API)
* **Entity Framework Core** (ORM para persistência de dados)
* **AutoMapper** (Mapeamento entre Entidades e DTOs/ViewModels)
* **JWT (JSON Web Tokens)** (Autenticação e Autorização)
* **xUnit & Moq** (Testes Unitários e de Integração)
* **Swagger/OpenAPI** (Documentação e testes da API)
* **Padrão Repository** (Abstração de acesso a dados)

---

## 🏗️ Estrutura do Projeto (Onde achar o quê)

O projeto foi desenhado focando em uma arquitetura limpa (N-Tier/Clean Architecture), separando as responsabilidades para facilitar a manutenção e os testes.

* **`Presentation/` (Apresentação)**
    * **`Controllers/`**: Onde as requisições HTTP chegam. Você encontrará os endpoints de `ClientesController`, `PetsController` e `AuthController` (para login e geração de JWT).
    * **`DTOs/` e `ViewModel/`**: Classes que definem exatamente quais dados entram (`ViewModels` de Create/Update) e quais dados saem (`Responses`) da API.
* **`Application/` (Aplicação)**
    * **`Services/`**: Contém a lógica de negócio do sistema (`ClientesService`, `PetsService`, `TokenService`, `FileStorageService`).
    * **`Mappings/`**: Configurações do AutoMapper (`AutoMapperProfile.cs`) que ensinam o sistema a converter um `ViewModel` em uma Entidade, e vice-versa.
* **`Domain/` (Domínio)**
    * **`Modelos/`**: As entidades centrais do sistema (`Clientes`, `Pets`).
    * **`Enums/`**: Enumerações como `PorteAnimalEnum` e `TipoAnimalEnum`.
    * **`Interfaces/`**: Contratos para os Repositórios e Serviços.
* **`Infraestrutura/` (Infra)**
    * **Contexto**: `GerenciamentoPetShopContext.cs` (A ponte com o banco de dados).
    * **Repositórios**: `ClientesRepository` e `PetsRepository` (Onde ficam os `Add`, `Update`, `Remove`, `Get` reais com o banco).
    * **`Migrations/`**: O histórico de criação de tabelas e modificações do banco de dados.

---

## 🚀 Como Executar o Projeto Localmente

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) (versão compatível com o projeto - 8.0 ou superior).
* Uma IDE como Visual Studio, VS Code ou Rider.
* Ferramenta de linha de comando do Entity Framework Core (`dotnet tool install --global dotnet-ef`).

### Passo a Passo

1.  **Clone e Restaure os Pacotes**
    Abra o terminal na pasta raiz do projeto e execute:
    ```bash
    dotnet restore
    ```

2.  **Configuração do Banco de Dados**
    Verifique o arquivo `appsettings.json` ou `appsettings.Development.json` na pasta do projeto principal (`Gerenciamento PetShop`) para confirmar a string de conexão (geralmente SQL Server ou SQLite local).

3.  **Crie as Tabelas (Migrations)**
    Para gerar o banco de dados e as tabelas com base nas entidades do sistema, execute o comando abaixo na raiz da solution (ou apontando para o projeto de infraestrutura):
    ```bash
    dotnet ef database update --project "Gerenciamento PetShop"
    ```

4.  **Rodando a API**
    Inicie o projeto com o comando:
    ```bash
    dotnet run --project "Gerenciamento PetShop"
    ```
    *A API será iniciada. Acesse a URL indicada no terminal (geralmente `http://localhost:5000/swagger` ou `https://localhost:5001/swagger`) para visualizar a interface interativa do Swagger.*

---

## 💾 Como Popular o Banco de Dados

Uma vez que a API esteja rodando e o Swagger aberto no seu navegador, você pode popular o banco de dados seguindo esta ordem (devido aos relacionamentos entre as tabelas):

1.  **Criar Clientes (Donos dos Pets):**
    * Encontre o endpoint `POST /api/clientes`.
    * Clique em "Try it out".
    * Envie o JSON com os dados do cliente (CPF, Nome, etc.).
    * *Anote o `Id` gerado para o cliente.*

2.  **Criar Pets:**
    * Encontre o endpoint `POST /api/pets`.
    * Clique em "Try it out".
    * Envie o JSON com os dados do pet (Nome, `TipoAnimalEnum`, `PorteAnimalEnum`) e **vincule ao cliente criado informando o `ClienteId` correspondente**.

3.  **Upload de Imagem (Opcional):**
    * Se o endpoint de criação ou edição de Pets tiver suporte `multipart/form-data` via `FileStorageService`, você pode fazer o upload de uma foto para o animalzinho.

*Nota: Se os endpoints exigirem autenticação (cadeado no Swagger), você precisará primeiro usar o endpoint `POST /api/auth` (ou equivalente) para gerar um Token JWT e inseri-lo no botão "Authorize" do Swagger no formato `Bearer {seu-token}`.*

---

## 🧪 Como Rodar os Testes

O projeto conta com uma suíte de testes (xUnit + Moq) no projeto `GerenciamentoPetShop.Testes`, cobrindo Controllers, Services e Repositories.

Para executar todos os testes e garantir que o sistema está íntegro:

```bash
dotnet test
```

Para ver o detalhamento dos testes pelo Visual Studio, basta abrir a aba "Test Explorer" (Gerenciador de Testes) e clicar em "Run All". Você verá a validação das regras de negócio, como paginação (`GetClientes_DeveRetornarListaDeClientes`), atualizações e retornos corretos dos Controllers.
