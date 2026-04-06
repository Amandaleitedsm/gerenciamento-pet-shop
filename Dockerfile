# ETAPA 1: Construção (Build) - Agora usando o .NET 10.0!
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copia TODOS os arquivos da pasta atual para dentro do Docker
COPY . .

# Vai direto na sub-pasta correta para compilar e publicar a API
RUN dotnet publish "Gerenciamento PetShop/Gerenciamento PetShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 2: Execução (Runtime) - Agora usando o .NET 10.0!
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

# Pega no código compilado da Etapa 1 e cola aqui
COPY --from=build /app/publish .

# Liga a API!
ENTRYPOINT ["dotnet", "Gerenciamento PetShop.dll"]